using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetNPC : MonoBehaviour, IMove
{
    public LayerMask ignoreMask;
    public LayerMask nodeMask;
    public float speed = 2;
    public List<Node> waypoints;
    public float speedRot;
    public bool readyToMove;
    public int _nextPoint = 0;
    private bool addedCost;
    public NameObject name;
    public GameObject myBody;
    [SerializeField] private List<GameObject> maleBodys;
    [SerializeField] private List<GameObject> femaleBodys;
    public Material[] materials;
    public Transform start;
    public Transform end;
    public Transform shop;
    public float probabilityToShop;
    public float probabilityLossToShop;
    public float maxTimeInShop;
    public float minTimeInShop;
    private float timeInShop;
    List<GameObject> childrens = new List<GameObject>();

    private void Awake()
    {
        if (GameObject.Find("Juanela de las Chozas"))
            GetComponent<Speak>().textUI = GameObject.Find("Juanela de las Chozas").GetComponent<Speak>().textUI;
        foreach (GameObject gameObject in maleBodys)
            childrens.Add(gameObject);
        foreach (GameObject gameObject in femaleBodys)
            childrens.Add(gameObject);
    }

    private void Update()
    {
        if (end != null)
            if (Vector3.Distance(end.position, transform.position) < 1)
                FindObjectOfType<NpcManager>().Reincarnate(gameObject);
        if (shop != null)
            if (Vector3.Distance(shop.position, transform.position) < 1)
            {
                timeInShop = Random.Range(minTimeInShop, maxTimeInShop);
                if (timeInShop <= 0)
                {
                    probabilityToShop -= probabilityLossToShop;
                    var manager = FindObjectOfType<NpcManager>();
                    manager.shopNodes.Add(shop.GetComponent<Node>());
                    shop = null;
                    timeInShop = 0;
                    manager.SetDestination(new List<Node>(manager.endNodes), gameObject);
                }
                else timeInShop += Time.deltaTime;
            }
    }

    public void ChangeBody()
    {
        myBody = childrens[Random.Range(0, childrens.Count)];
        foreach (GameObject gameObject in childrens)
        {
            if (gameObject.gameObject != myBody)
                gameObject.gameObject.SetActive(false);
            else gameObject.gameObject.SetActive(true);
        }
        if (myBody != null)
            myBody.GetComponent<SkinnedMeshRenderer>().material = materials[Random.Range(0, materials.Length)];
        if (name != null)
        {
            if (maleBodys.Contains(myBody))
                ChangeName(name.MaleName);
            if (femaleBodys.Contains(myBody))
                ChangeName(name.FemaleName);
        }
    }

    void ChangeName(List<string> names)
    {
        List<string> myNames = new List<string>();
        var trans = Random.Range(0, 100);
        if (trans <= 5)
        {
            if (names == name.MaleName)
                foreach (string name in name.FemaleName)
                    myNames.Add(name);
            else
                foreach (string name in name.MaleName)
                    myNames.Add(name);
        }
        else foreach (string name in names)
                myNames.Add(name);
        foreach (string name in name.MixedName)
            myNames.Add(name);
        gameObject.name = myNames[Random.Range(0, myNames.Count)] + " " + name.Surname[Random.Range(0, name.Surname.Count)];
    }
    public void FindPath()
    {
        GetComponent<AgentAs>().PathFindingAstar();
    }
    public void Move(Vector3 dir)
    {
        
    }
    public void MoveAO(Vector3 dir)
    {
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.2f);
        Quaternion lookRotation = Quaternion.LookRotation(GetComponent<StreetNPCController>().target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
    public void MoveTransfrom(Vector3 dir)
    {
        dir.y = 0;
        transform.position += dir * speed * Time.deltaTime;
    }
    public void SetWayPoints(List<Node> newPoints)
    {
        _nextPoint = 0;
        if (newPoints.Count == 0) return;
        waypoints = newPoints;
        var pos = waypoints[_nextPoint].transform.position;
        pos.y = transform.position.y;
        transform.position = pos;
        readyToMove = true;
    }
}
