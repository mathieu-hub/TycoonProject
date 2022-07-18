using UnityEngine;
using UnityEngine.AI;

public class PeopleController : MonoBehaviour
{
    public Camera cam;

    public enum typeOfPeople {IA, Playable};
    public typeOfPeople _typeOfPeople;

    public NavMeshAgent agent;

    void Update()
    {
        if (_typeOfPeople == typeOfPeople.Playable)
        {
            ControllingPeople();
        }       
    }

    private void ControllingPeople() //Fonction permettant de prendre le contrôle de la destination d'un People.
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