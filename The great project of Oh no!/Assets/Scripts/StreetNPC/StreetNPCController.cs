using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetNPCController : MonoBehaviour
{
    public Transform target;
    public float timePrediction;
    public float radius;
    public float avoidWeight;
    public LayerMask mask; 
    public LayerMask flockingMaskPredator;
    public float separationWeight;
    public float alineationWeight;
    public float leaderWeight;
    public float cohesionWeight;
    public float predatorWeight;
    ISteering _sb;
    StreetNPC _StreetNPC;
    Vector3 _dir = Vector3.zero;
    public void ChangeSteering(ISteering newSteering)
    {
        _sb = newSteering;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _dir * 2);
    }
}
