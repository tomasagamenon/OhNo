using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcManager : MonoBehaviour
{
    public Node[] endNodes;
    public Node[] nodes;
    public List<Node> shopNodes;
    public float spawnTime;
    public Settings populationSettings;
    public int population;
    public GameObject npc;
    public GameObject canvas;

    private void Awake()
    {
        canvas.SetActive(true);
        population = populationSettings.population;
        List<Node> x = new List<Node>(endNodes);
        List<Node> x2 = new List<Node>(nodes);
        for (int i = 0; i < population; i++)
        {
            int a = Random.Range(0, x2.Count);
            NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
            int vertexIndex = Random.Range(0, triangulation.vertices.Length);
            var b = Instantiate(npc, triangulation.vertices[vertexIndex], transform.rotation);
            SetDestination(x, b);

            x = new List<Node>(endNodes);
        }
    }

    public void ChangePopulation(int newPopulation)
    {
        if (newPopulation != population)
        {
            if (newPopulation < population)
            {
                var pop = population;
                population = newPopulation;
                for (int i = pop; i > newPopulation; i--)
                {
                    var npcs = FindObjectsOfType<StreetNPC>();
                    Destroy(npcs[i].gameObject);
                }
            }
            else if (newPopulation > population)
            {
                List<Node> x = new List<Node>(endNodes);
                List<Node> x2 = new List<Node>(nodes);
                var pop = population;
                population = newPopulation;
                for (int i = pop; i < newPopulation; i++)
                {
                    int a = Random.Range(0, x2.Count);
                    NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
                    int vertexIndex = Random.Range(0, triangulation.vertices.Length);
                    var b = Instantiate(npc, triangulation.vertices[vertexIndex], transform.rotation);
                    SetDestination(x, b);

                    x = new List<Node>(endNodes);
                }
            }
        }
    }

    public void Reincarnate(GameObject b)
    {
        List<Node> x = new List<Node>(endNodes);
        int a = Random.Range(0, x.Count);
        b.transform.position = x[a].transform.position;
        b.GetComponent<StreetNPC>().start = x[a].transform;
        b.GetComponent<StreetNPC>().myBody = null;
        x.Remove(x[a]);
        SetDestination(x, b);
    }

    public void SetDestination(List<Node> x, GameObject b)
    {
        b.GetComponent<StreetNPC>().ChangeBody();
        var end = x[Random.Range(0, x.Count)].transform;
        var shop = Random.Range(0, 100);
        if (b.GetComponent<StreetNPC>().probabilityToShop - shop > 0)
        {
            var node = shopNodes[Random.Range(0, shopNodes.Count)];
            end = node.transform;
            b.GetComponent<StreetNPC>().shop = end;
            shopNodes.Remove(node);
        }
        else
        {
            b.GetComponent<StreetNPC>().end = end;
        }
        b.GetComponent<NavMeshAgent>().SetDestination(end.position);
        float prob = 0;
        foreach (Walk w in walks)
            prob += w.probability;
        var random = Random.Range(0, prob);
        float z = 0;
        foreach (Walk w in walks)
        {
            z += w.probability;
            if (random - z <= 0)
            {
                b.GetComponent<Animator>().SetBool(w.walk, true);
                break;
            }
        }
    }

    [System.Serializable]
    public class Walk
    {
        public string walk;
        public float probability;
    }

    public Walk[] walks;
}