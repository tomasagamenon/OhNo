using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private Enemy core;
    private bool isGuarding;
    private void Awake()
    {
        core = GetComponent<Enemy>();
    }
    private void OnEnable()
    {
        Debug.Log("EnemyChase activado");
    }
    private void Update()
    {
        if (core.nma.remainingDistance <= core.effectiveRange)
        {
            Vector3 direction = core.player.position - transform.position;
            bool ray = Physics.Raycast(transform.position, direction, out RaycastHit hit, core.sightLayers);
            if (ray && hit.collider.CompareTag("Player"))
            {
                core.Decide();
                Debug.Log("Se encontro al jugador");
            } else if(!isGuarding)
            {
                isGuarding = true;
                StartCoroutine(Guard());
            }
        }
    }
    public void Chase(Vector3 position)
    {
        core.nma.SetDestination(position);
        core.nma.stoppingDistance = (core.effectiveRange);
    }
    private void OnDisable()
    {
        Debug.Log("EnemyChase desactivado");
        StopCoroutine(Guard());
    }
    IEnumerator Guard()
    {
        //animacion loopeada de mirar hacia los costados
        Debug.Log("Se inicia la coroutine Guard");
        new WaitForSeconds(3f);
        Debug.Log("No se encontro al jugador");
        isGuarding = false;
        core.Retreat();
        yield return null;
    }
}
