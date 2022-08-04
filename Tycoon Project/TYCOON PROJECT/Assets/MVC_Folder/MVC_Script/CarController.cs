using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    public NavMeshAgent agentCar;
    public bool canDrive = false;

    public Transform targetedParkingPlace;
    public bool hasTargetingPP = false;

    void Update()
    {
        if (targetedParkingPlace != null)
        {
            hasTargetingPP = true;
        }

        if (canDrive)
        {
            agentCar.SetDestination(targetedParkingPlace.position);
        }
    }
}
