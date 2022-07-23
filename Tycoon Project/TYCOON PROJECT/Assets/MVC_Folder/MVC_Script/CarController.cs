using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    public ParkingPlace _ParkingPlace;

    public NavMeshAgent agentCar;
    public bool canDrive = false;

    public Transform targetedParkingPlace;
    public bool hasTargetingPP = false;

    void Update()
    {
        if (!canDrive)
        {
            if (targetedParkingPlace != null)
            {
                hasTargetingPP = true;
                canDrive = true;
            }
        }

        if (canDrive)
        {
            agentCar.SetDestination(targetedParkingPlace.position);
        }
    }

    public void FindParkingPlace()
    {
        
    }
}
