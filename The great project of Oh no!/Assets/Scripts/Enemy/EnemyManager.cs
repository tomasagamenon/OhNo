using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] fleePositions;
    public Transform[] retreatPositions;

    public Vector3 RetreatPosition()
    {
        Vector3 position;
        position = retreatPositions[Random.Range(0, retreatPositions.Length)].position;
        return position;
    }
}
