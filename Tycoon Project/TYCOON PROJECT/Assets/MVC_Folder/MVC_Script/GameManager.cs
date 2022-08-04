using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Script References")]
    public BuildingManager _BuildingManager;
    public CarController _CarController;

    [Header("Objects")]
    public GameObject carPrefab;

    [Header("Actualisable Objects")]
    public GameObject pendingCar;

    [Header("Spawner")]
    public Transform carSpawner;

    [Header("DEBUG")]
    public bool launchParkingUpdate = false;

    [Header("DATA")]
    public float entrancePrice;
    public int numberOfParkPlace;

    private void Update()
    {
        if (launchParkingUpdate)
        {
            ParkingUpdate();
        }
    }

    public void ParkingUpdate()
    {
        //Actualisation de la Data
        numberOfParkPlace = _BuildingManager.parkingPlace.Count;

        for (int i = 0; i < _BuildingManager.parkingPlace.Count; i++)
        {
            if (_BuildingManager.parkingPlace[i].GetComponent<ParkingPlace>().isAvailable)
            {
                Debug.Log("SO6");

                if (pendingCar == null)
                {
                    _BuildingManager.parkingPlace[i].GetComponent<ParkingPlace>().isAvailable = false;

                    pendingCar = Instantiate(carPrefab, carSpawner.position, Quaternion.identity);

                    pendingCar.GetComponent<CarController>().targetedParkingPlace = 
                        _BuildingManager.parkingPlace[i].GetComponent<ParkingPlace>().positionToPark;
                    
                    Debug.Log("CHO7");
                }

                if (pendingCar.GetComponent<CarController>().hasTargetingPP) //Can works cause pendingCar != null
                {
                    Debug.Log("TR8");
                    pendingCar = null;
                }
            }
        }
    }
}
