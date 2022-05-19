using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar<T>
{
    public delegate bool Satisfies(Node curr);
    public delegate List<Node> GetNeighbours(Node curr);
    public delegate float Heuristic(Node current);
    public delegate float GetCost(Node p, Node c);
    public List<global::Node> Run(global::Node start, Satisfies satisfies, GetNeighbours getNeighbours, GetCost getCost, Heuristic heuristic, int watchDong = 500)
    {
        Dictionary<Node, float> cost = new Dictionary<Node, float>();
        Dictionary<Node, Node> parents = new Dictionary<Node, Node>();
        PriorityQueue<Node> pending = new PriorityQueue<Node>();
        HashSet<Node> visited = new HashSet<Node>();
        pending.Enqueue(start, 0);
        cost.Add(start, 0);
        while (!pending.IsEmpty)
        {
            Node current = pending.Dequeue();
            watchDong--;
            if (watchDong <= 0) return new List<Node>();
            if (satisfies(current))
            {
                return ConstructPath(current, parents);
            }
            visited.Add(current);
            List<Node> neighbours = getNeighbours(current);
            for (int i = 0; i < neighbours.Count; i++)
            {
                Node node = neighbours[i];
                if (visited.Contains(node)) continue;
                float nodeCost = getCost(current, node);
                float totalCost = cost[current] + nodeCost + node.cost;
                if (cost.ContainsKey(node) && cost[node] < totalCost) continue;
                cost[node] = totalCost;
                parents[node] = current;
                pending.Enqueue(node, totalCost + heuristic(node));
            }
        }
        return new List<Node>();
    }

    List<Node> ConstructPath(Node end, Dictionary<Node, Node> parents)
    {
        var path = new List<Node>();
        path.Add(end);
        while (parents.ContainsKey(path[path.Count - 1]))
        {
            var lastNode = path[path.Count - 1];
            path.Add(parents[lastNode]);
        }
        path.Reverse();
        return path;
    }
}
