using UnityEngine;
using UnityEngine.AI;

public class PeopleController : MonoBehaviour
{
    public Camera cam;

    public NavMeshAgent agent;

    void Update()
    {
        //Récupère la position du pointeur de la souris dans l'espace lors du clic.
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point); //Octroie la position comme destination à l'agent.
            }
        }
    }
}