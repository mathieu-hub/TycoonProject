using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("SCRIPT REFERENCES")]
    public BuildingManager _BuildingManager;
    public CarController _CarController;
    public PeopleController _PeopleController;

    [Header("OBJECTS")]
    public Camera mainCam;
    [Space(4)]
    public GameObject carPrefab;
    public GameObject peoplePrefab;

    [Header("ACTUALISABLE OBJECTS")]
    public GameObject pendingCar;

    [Header("SPAWNER")]
    public Transform carSpawner;

    [Header("DEBUG")]
    public bool launchParkingUpdate = false;

    [Header("PARAMETERS")]
    public float timeBtwnCarSpawn;
    public float entrancePrice;

    [Header("DATA")]
    public int numberOfParkPlace;

    //Locals Variable
    private bool carSpawned = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Plus d'une instance de GameManager dans la scène !");
            return;
        }

        instance = this;
    }

    private void Update()
    {
        if (launchParkingUpdate)
        {
            ParkingUpdate();
        }
    }

    //PARKING AREA
    public void ParkingUpdate()
    {
        //Actualisation de la Data
        numberOfParkPlace = _BuildingManager.parkingPlace.Count;

        for (int i = 0; i < _BuildingManager.parkingPlace.Count; i++)
        {
            if (_BuildingManager.parkingPlace[i].GetComponent<ParkingPlace>().isAvailable)
            {
                if (pendingCar == null)
                {
                    _BuildingManager.parkingPlace[i].GetComponent<ParkingPlace>().isAvailable = false; //Check si place de parking disponible

                    pendingCar = Instantiate(carPrefab, carSpawner.position, Quaternion.identity); //Instantiate car prefab

                    carSpawned = true; //A Car appear

                    pendingCar.GetComponent<CarController>().targetedParkingPlace = 
                        _BuildingManager.parkingPlace[i].GetComponent<ParkingPlace>().positionToPark; //Car prend la position de la PP comme targetPoint
                    
                }

                if (pendingCar.GetComponent<CarController>().hasTargetingPP) //La voiture instanciée a trouvé une place de parking
                {
                    if (carSpawned)
                    {
                        StartCoroutine(DelayToSpawnCar());
                        Debug.Log("SO6");
                        carSpawned = false;
                    }
                }
            }
        }
    }

    IEnumerator DelayToSpawnCar()
    {
        yield return new WaitForSeconds(timeBtwnCarSpawn);
        pendingCar = null;
        Debug.Log("CHO7");
        carSpawned = true;
    }
}
