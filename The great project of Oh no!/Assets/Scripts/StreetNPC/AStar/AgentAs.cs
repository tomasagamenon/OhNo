using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AgentAs : MonoBehaviour
{
    public float distanceMax;
    public float radius;
    public Vector3 offset;
    public Node init;
    public Node finit;
    public StreetNPC pj;
    List<Node> _list;
    List<Vector3> _listVector;
    AStar<Node> _aStar = new AStar<Node>();
    public LayerMask mask;
    public void PathFindingAstar()
    {
        _list = _aStar.Run(init, Satisfies, GetNeighbours, GetCost, Heuristic);
        pj.SetWayPoints(_list);
    }

    float GetCost(Node p, Node c)
    {
        return Vector3.Distance(p.transform.position, c.transform.position);
    }

    float Heuristic(Node curr)
    {
        return Vector3.Distance(curr.transform.position, finit.transform.position);
    }
    List<Node> GetNeighbours(Node curr)
    {
        return curr.neightbourds;
    }
    bool Satisfies(Node curr)
    {
        return curr == finit;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (init != null)
            Gizmos.DrawSphere(init.transform.position + offset, radius);
        if (finit != null)
            Gizmos.DrawSphere(finit.transform.position + offset, radius);
        if (_list != null)
        {
            Gizmos.color = Color.blue;
            foreach (var item in _list)
            {
                if (item != init && item != finit)
                    Gizmos.DrawSphere(item.transform.position + offset, radius);
            }
        }
        if (_listVector != null)
        {
            Gizmos.color = Color.green;
            foreach (var item in _listVector)
            {
                if (item != init.transform.position && item != finit.transform.position)
                    Gizmos.DrawSphere(item + offset, radius);
            }
        }

    }
}
