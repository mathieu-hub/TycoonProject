using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    public NavMeshAgent agentCar;
    public bool canDrive = false;

    public Transform targetedParkingPlace;
    public bool hasTargetingPP = false;

    public bool isParked = false;
    public bool containPeoples = true;
    public Transform peopleSpawner;

    void Update()
    {
        if (targetedParkingPlace != null)
        {
            hasTargetingPP = true;
        }

        if (canDrive) //Si la voiture peut rouler
        {
            agentCar.SetDestination(targetedParkingPlace.position); //La voiture se rend à la place de parking attribuée (cf.GameManager)
        }

        if (gameObject.transform.position.x == targetedParkingPlace.position.x && gameObject.transform.position.z == targetedParkingPlace.position.z)
        {
            gameObject.transform.rotation = targetedParkingPlace.rotation;
            isParked = true;
        }

        if (isParked)
        {
            if (containPeoples)
            {
                SpawnPeoplesFromCar();
            }
        }
    }

    public void SpawnPeoplesFromCar()
    {
        containPeoples = false;
        canDrive = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        Instantiate(GameManager.instance.peoplePrefab, peopleSpawner.position, Quaternion.identity);
    }
}
