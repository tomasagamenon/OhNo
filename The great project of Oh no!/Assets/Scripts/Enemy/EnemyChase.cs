using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private Enemy core;
    private void Awake()
    {
        core = GetComponent<Enemy>();
    }
    private void OnEnable()
    {
        Debug.Log("EnemyChase activado");
        //core.nma.SetDestination(core.player.position);
        //deberia chequearse cada tanto si esta en rango de vision, en caso contrario deberia ir hacia el de fuera de vision
        //pero que siga un poco antes persiguiendolo, quizas
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, core.player.position);
        if (distance < core.effectiveRange - 10)
            core.Decide();
    }
}
