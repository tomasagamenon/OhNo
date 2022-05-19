using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlee : MonoBehaviour
{
    //hay que hacer que el enemigo huya hacia un punto específico en el mapa, que puede ser el más cercano en la dirección opuesta al jugador
    private Enemy core;
    public Transform left;
    public Transform right;

    private Transform[] fleeRoutes;

    private void Awake()
    {
        core = GetComponent<Enemy>();
        fleeRoutes = core.enemyManager.fleePositions;
    }
    private void OnEnable()
    {
        Debug.Log(this.name + " activado");
        CalculateRoute();
    }
    private void Update()
    {
        if (!core.nma.pathPending && core.nma.hasPath && core.nma.remainingDistance < 0.5f)
        {
            //avisar a quien corresponda que el enemigo "muere"
            gameObject.SetActive(false);
        }
    }
    public void CalculateRoute()
    {
        //se fija primero cuales estan en la direccion opuesta a la posicion del jugador, despues una por una cual es la mas cercana
        //quizas se podria hacer segun cual tiene un menor recorrido, es decir, 
        //la distancia que tendria que reccorer para llegar al punto, en vez de la distancia global
        List<Transform> posibilities = new List<Transform>();
        foreach (Transform route in fleeRoutes)
        {
            Vector3 direction = core.player.position - transform.position;
            Vector3 fleeDirection = route.position - transform.position;
            float dot = Vector3.Dot(direction.normalized, fleeDirection.normalized);//direccion del jugador y despues direccion del punto
            if (dot <= 0)
                posibilities.Add(route);
        }
        Transform nearestRoute = posibilities[0];
        float nearestDistance = Vector3.Distance(transform.position, nearestRoute.position);
        foreach (Transform posibility in posibilities)
        {
            float actualDistance = Vector3.Distance(transform.position, posibility.position);
            if (actualDistance <= nearestDistance)
            {
                nearestRoute = posibility;
                nearestDistance = actualDistance;
            }
        }
        core.nma.SetDestination(nearestRoute.position);
    }
}
