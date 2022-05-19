using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking :ISteering
{
    Transform _transform;
    float _range;
    LayerMask _mask;
    LayerMask _maskPredator;
    Transform _leaderToFollow;
    float _separationWeight;
    float _alineationWeight;
    float _leaderWeight;
    float _cohesionWeight;
    float _predatorWeight;
    Vector3 _dir;
    public Flocking(Transform transform, float range, LayerMask mask, LayerMask maskPredator, Transform leaderToFollow, float separationWeight, float alineationWeight, float leaderWeight, float cohesionWeight, float predatorWeight)
    {
        _transform = transform;
        _range = range;
        _mask = mask;
        _maskPredator = maskPredator;
        _leaderToFollow = leaderToFollow;
        _separationWeight = separationWeight;
        _alineationWeight = alineationWeight;
        _leaderWeight = leaderWeight;
        _cohesionWeight = cohesionWeight;
        _predatorWeight = predatorWeight;
    }
    public Vector3 GetDir()
    {
        Collider[] entities = Physics.OverlapSphere(_transform.position, _range, _mask);
        Collider[] predators = Physics.OverlapSphere(_transform.position, _range, _maskPredator);
        var separation = GetSeparationDir(_transform.position, entities, _range) * _separationWeight;
        var predator = GetSeparationDir(_transform.position, predators, _range) * _predatorWeight;
        var cohesion = GetCohesionDir(_transform.position, entities) * _cohesionWeight;
        var alineation = GetAlineationDir(_transform.position, entities) * _alineationWeight;
        var leader = GetLeaderDir(_transform.position, _leaderToFollow.position) * _leaderWeight;

        _dir = (separation + cohesion + alineation + leader + predator);
        if (_dir.magnitude < 0.5f)
            return Vector3.zero;
        return _dir.normalized;
    }

    Vector3 GetSeparationDir(Vector3 origin, Collider[] entities, float maxDistance)
    {
        Vector3 separation = Vector3.zero;
        for (int i = 0; i < entities.Length; i++)
        {
            if (entities[i].transform.position == origin) continue;
            Vector3 dirToOrigin = origin - entities[i].transform.position;
            float distance = dirToOrigin.magnitude;
            if (maxDistance < distance)
                distance = maxDistance - 0.1f;
            dirToOrigin = dirToOrigin.normalized * (maxDistance - distance);
            separation += dirToOrigin;
        }
        return separation.normalized;
    }
    Vector3 GetCohesionDir(Vector3 origin, Collider[] entities)
    {
        Vector3 averagePos = Vector3.zero;
        for (int i = 0; i < entities.Length; i++)
        {
            if (entities[i].transform.position == origin) continue;
            averagePos += entities[i].transform.position;
        }
        averagePos /= entities.Length;
        return (averagePos - origin).normalized;
    }
    Vector3 GetAlineationDir(Vector3 origin, Collider[] entities)
    {
        Vector3 averageDir = Vector3.zero;
        for (int i = 0; i < entities.Length; i++)
        {
            if (entities[i].transform.position == origin) continue;
            averageDir += entities[i].transform.forward;
        }
        return averageDir / entities.Length;
    }
    Vector3 GetLeaderDir(Vector3 origin, Vector3 leader)
    {
        return (leader - origin);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_transform.position, _range);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_transform.position, _dir * _range);
    }
}
