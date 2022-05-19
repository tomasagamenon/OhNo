using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public Transform player;

    public Transform lastPosition;
    public float sightRange;
    private bool checkSight;
    public float sightTimer;
    private float _sightTimer;
    public LayerMask sightLayers;

    public bool isFleeing;

    public float effectiveRange;
    private bool checkDistance = true;
    public float checkTimer;
    private float _checkTimer;

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
        enemyCover = GetComponent<EnemyCover>();
        enemyChase = GetComponent<EnemyChase>();
        enemyFlee = GetComponent<EnemyFlee>();
        _sightTimer = sightTimer;
        _checkTimer = checkTimer;
        lastBehavior = enemyFlee; //temporal
    }
    private void Update()
    {
        if (!checkSight)
        {
            _sightTimer -= Time.deltaTime;
            if(_sightTimer <= 0)
            {
                checkSight = true;
                _sightTimer = sightTimer;
            }
        } else if (!isFleeing)
        {
            Vector3 direction = player.position - transform.position;
            if(Physics.Raycast(transform.position, direction, out RaycastHit hit, sightRange, sightLayers))
            {
                if (!hit.collider.gameObject.CompareTag("Player"))
                {
                    lastPosition = player;
                }
            }
        }
        if (!checkDistance)
        {
            _checkTimer -= Time.deltaTime;
            if(_checkTimer <= 0)
            {
                checkDistance = true;
                _checkTimer = checkTimer;
            }
        }else if (!isFleeing)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if(distance <= effectiveRange)
            {
                GoChase();
            }
            checkDistance = false;
        }
    }


    MonoBehaviour lastBehavior;
    public void Decide()
    {
        Debug.Log("El enemigo está decidiendo");
        
    }
    public void Shoot()
    {
        lastBehavior.enabled = false;
    }
    public void TakeCover()
    {
        //lastBehavior.enabled = false;
        enemyCover.enabled = true;
        lastBehavior = enemyCover;
    }
    public void GoChase()
    {
        enemyCover.enabled = false;
        lastBehavior.enabled = false;
        enemyChase.enabled = true;
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
    }
    public void ReadyToFlee()
    {
        isFleeing = true;
        lastBehavior.enabled = false;
        enemyFlee.enabled = true;
    }
}
