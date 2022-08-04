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
                if (pendingCar == null)
                {
                    _BuildingManager.parkingPlace[i].GetComponent<ParkingPlace>().isAvailable = false; //Check si place de parking disponible

                    pendingCar = Instantiate(carPrefab, carSpawner.position, Quaternion.identity); //Instantiate car prefab

                    pendingCar.GetComponent<CarController>().targetedParkingPlace = 
                        _BuildingManager.parkingPlace[i].GetComponent<ParkingPlace>().positionToPark; //Car prend la position de la PP comme targetPoint
                    
                }

                if (pendingCar.GetComponent<CarController>().hasTargetingPP) //La voiture instanciée a trouvé une place de parking
                {
                    pendingCar = null;
                }
            }
        }
    }
}
