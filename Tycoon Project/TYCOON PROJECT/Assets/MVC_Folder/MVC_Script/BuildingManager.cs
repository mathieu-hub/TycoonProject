using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class BuildingManager : MonoBehaviour
{
    public NavMeshSurface surfaceRoad;
    public GameObject navMeshRoad;

    public List<GameObject> parkingPlace = new List<GameObject>();

    public GameObject[] objects;
    private GameObject pendingObject;

    private Vector3 pos;

    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;
    
    public float rotateAmount;

    public float gridSize;
    bool gridOn = true;
    [SerializeField] private Toggle gridToggle;


    private void Start()
    {
        
    }

    private void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.GetComponent<Collider>().enabled = false;

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
                pendingObject.GetComponent<Collider>().enabled = true;

                PlaceObject();

                surfaceRoad.BuildNavMesh();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }
        }
    }

    public void PlaceObject()
    {
        pendingObject = null;
    }

    public void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
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
        pendingObject = Instantiate(objects[index], pos, transform.rotation);
        
        if (index == 1 || index == 2)
        {
            pendingObject.transform.SetParent(navMeshRoad.transform);

            if (index == 1)
            {
                parkingPlace.Add(pendingObject);
            }
        }
    }

    public void ToggleGrid()
    {
        if (gridToggle.isOn)
        {
            gridOn = true;
        }
        else { gridOn = false; }
    }

    float RoundToNearestGrid(float pos)
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
