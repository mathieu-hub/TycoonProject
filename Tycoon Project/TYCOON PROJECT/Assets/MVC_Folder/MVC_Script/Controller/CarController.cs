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

        if (gameObject.transform.position.x == targetedParkingPlace.position.x && gameObject.transform.position.z == targetedParkingPlace.position.z)
        {
            gameObject.transform.rotation = targetedParkingPlace.rotation;
        }
    }
}
