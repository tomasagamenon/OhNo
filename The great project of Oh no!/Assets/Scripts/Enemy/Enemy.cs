using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _nmaDistance;
    public Transform player;

    public Transform lastPosition;
    public float sightRange;
    //private bool checkSight;
    //public float sightTimer;
    //private float _sightTimer;
    public LayerMask sightLayers;


    public float effectiveRange;
    //private bool checkDistance = true;
    //public float checkTimer;
    //private float _checkTimer;

    public float senseTimer;
    private float _senseTimer;
    private bool checkSenses;

    private bool isSearching;
    private bool isFleeing;

    public float coverRange;
    public LayerMask coverLayer;

    public Transform destino;

    public NavMeshAgent nma;

    public EnemyCover enemyCover;
    public EnemyChase enemyChase;
    public EnemyFlee enemyFlee;
    public EnemyManager enemyManager;
    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        nma = GetComponent<NavMeshAgent>();
        nma.ResetPath();
        enemyCover = GetComponent<EnemyCover>();
        enemyChase = GetComponent<EnemyChase>();
        enemyFlee = GetComponent<EnemyFlee>();
        //_sightTimer = sightTimer;
        //_checkTimer = checkTimer;
        _senseTimer = senseTimer;
        lastBehavior = enemyFlee; //temporal
    }
    private void Update()
    {
        _nmaDistance = nma.remainingDistance;
        if (!checkSenses)
        {
            _senseTimer -= Time.deltaTime;
            if(_senseTimer <= 0)
            {
                Debug.Log("Se chequean los sentidos");
                checkSenses = true;
                _senseTimer = senseTimer;
            }
        } else if (!isFleeing)
        {
            lastPosition = null;
            Vector3 direction = player.position - transform.position;
            bool ray = Physics.Raycast(transform.position, direction, out RaycastHit hit, sightRange, sightLayers);
            if (ray && hit.collider.CompareTag("Player"))
            {
                isSearching = false;
                Debug.DrawRay(transform.position, direction, Color.green, 1f);
                float distance = Vector3.Distance(transform.position, player.position);
                _distance = distance;
                if (distance > effectiveRange)
                {
                    GoChase(player);
                    Debug.Log("El jugador es perseguido");
                }
                else
                    nma.ResetPath();
            }
            else if(!isSearching)
            {
                Debug.DrawRay(transform.position, direction, Color.red, 1f);
                lastPosition = player;
                GoChase(lastPosition);
                isSearching = true;
                Debug.Log("Se perdio de vista al jugador y se lo persigue");
                //hace lo que sea que vaya a hacer cuando lo pierda de vista
            }
        }
    }
    MonoBehaviour lastBehavior;
    public void Decide()
    {
        enemyChase.enabled = false;
        Debug.Log("El enemigo está decidiendo");
        isSearching = false;
    }
    public void Shoot()
    {
        lastBehavior.enabled = false;
    }
    public void TakeCover()
    {
        lastBehavior.enabled = false;
        enemyCover.enabled = true;
        lastBehavior = enemyCover;
    }
    public void GoChase(Transform transform)
    {
        enemyCover.enabled = false;
        lastBehavior.enabled = false;
        enemyChase.enabled = true;
        enemyChase.Chase(transform.position);
        lastBehavior = enemyChase;
    }
    public void Flee()
    {
        isFleeing = true;
        enemyCover.enabled = false;
        lastBehavior.enabled = false;
        enemyFlee.enabled = true;
        lastBehavior = enemyFlee;
    }
    public void Walk()
    {
        nma.SetDestination(destino.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 10);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 20);
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(destino.position, new Vector3(0.5f,0.5f,0.5f));
        if (enemyCover.goingTo != null)
            Gizmos.DrawWireCube(enemyCover.goingTo.position, new Vector3(0.5f, 0.5f, 0.5f));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, effectiveRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    public void ReadyToFlee()
    {
        isFleeing = true;
        lastBehavior.enabled = false;
        enemyFlee.enabled = true;
    }
    public void Retreat()
    {
        isSearching = false;
        enemyChase.enabled = false;
        nma.SetDestination(enemyManager.RetreatPosition());
        //se dirige a una ubicacion a vigilar si aparece el jugador
    }
}
