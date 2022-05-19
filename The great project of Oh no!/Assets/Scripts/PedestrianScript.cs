using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianScript : MonoBehaviour
{
    public Transform[] wayPoints;

    private int _actualPoint;
    private int _pathEnd;

    public NavMeshAgent navMeshAgent;

    private void Start()
    {
        _actualPoint = 0;
        _pathEnd = wayPoints.Length - 1;
    }
    void Update()
    {
        Transform actualPath = wayPoints[_actualPoint];
        navMeshAgent.SetDestination(actualPath.position);

        if (!navMeshAgent.pathPending && navMeshAgent.hasPath && navMeshAgent.remainingDistance < 0.5f)
        {
            if (_actualPoint >= 0 && _actualPoint < _pathEnd)
            {
                _actualPoint++;
            }
            else if (_actualPoint >= _pathEnd)
            {
                _actualPoint = 0;
            }
        }
    }
}
