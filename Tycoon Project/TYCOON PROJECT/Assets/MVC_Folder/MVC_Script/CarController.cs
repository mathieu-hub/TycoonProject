using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    public NavMeshAgent agentCar;
    public bool canDrive = true;

    public Transform targetedParkingPlace;

    void Update()
    {
        if (canDrive)
        {
            agentCar.SetDestination(targetedParkingPlace.position);
        }
    }

    public void FindParkingPlace()
    {

    }
}
