using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BuildingManager : MonoBehaviour
{
    public NavMeshSurface surfaceRoad;
    public GameObject navMeshRoad;
    [Space(10)]
    public UI_ParkingCounter ui_ParkingCounter;

    public List<GameObject> parkingPlace = new List<GameObject>();

    public GameObject[] buildableObject;
    [SerializeField]
    private GameObject pendingObject;

    private Vector3 pos;

    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;
    
    public float rotateAmount;

    public float gridSize;
    bool gridOn = true;
    [SerializeField] private Toggle gridToggle;


    private void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.GetComponent<Collider>().enabled = false; //Désactive le collider du pendingObject pour éviter l'empilement.

            if (gridOn)
            {
                pendingObject.transform.position = new Vector3(
                    RoundToNearestGrid(pos.x),
                    RoundToNearestGrid(pos.y),
                    RoundToNearestGrid(pos.z)
                    );
            }
            else { pendingObject.transform.position = pos; }


            if (Input.GetMouseButtonDown(0))
            {
                pendingObject.GetComponent<Collider>().enabled = true; //Réactive le collider du pendingObject

                PlaceObject();

                surfaceRoad.BuildNavMesh(); //Bake du navMesh des Roads et des ParkingsPlaces.
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DeleteSelectObject();
            }
        }
    }

    public void PlaceObject()
    {
        pendingObject = null; //Vide le contenu de PendingObject. 
    }

    public void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount); //Rotation de l'objet sélectionné.
    }

    public void DeleteSelectObject()
    {
        if (pendingObject.CompareTag("ParkPlace"))
        {
            ui_ParkingCounter.UIdeleteParkPlace();
        }
        Destroy(pendingObject);
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(buildableObject[index], pos, transform.rotation); //Instantiation de l'objet sélectionné selon l'index des objets référencés.
        
        if (index == 0 || index == 1 || index == 2)
        {
            //Activation et set la grid sur 10 pour le snapping
            gridSize = 10;
            gridOn = true; 

            pendingObject.transform.SetParent(navMeshRoad.transform); //Place les objets en parents du NavMeshRoad qui contient le navmesh pour les peoples.

            if (index == 1) //Lorsque l'objet parkingPlace est sélectionné, il est ajouté dans la liste de place de parking.
            {
                parkingPlace.Add(pendingObject);
                ui_ParkingCounter.UIaddParkPlace(); //Ajoute une place au compteur de place Total (UI).
            }
        }
        else
        {
            gridSize = 0.5f;

            if (gridToggle.isOn)
            {
                gridOn = true;
            }
            else { gridOn = false; }
        }
    }

    public void ToggleGrid() //Activation et désactivation de la grid (Fonction appelée depuis le Toggle)
    {
        if (gridToggle.isOn)
        {
            gridOn = true;
        }
        else { gridOn = false; }
    }

    float RoundToNearestGrid(float pos) //Arondissement pour snap les objets sur la grid
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }
}
