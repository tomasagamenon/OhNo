using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruebaaaaaa : MonoBehaviour
{
    public Transform player;
    public Vector3 asdf;
    public Transform origin;
    public LayerMask layer;
    public void AAAAAA()
    {
        /*if (Physics.Raycast(origin.position, origin.forward, out RaycastHit hit, 20f, layer))
        {
            Debug.Log("chocaste");
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Jugador");
                Debug.DrawRay(origin.position, origin.forward * 20f, Color.green, 5f);
            }
            else
                Debug.DrawRay(origin.position, origin.forward * 20f, Color.red, 5f);
        }
        else
            Debug.DrawRay(origin.position, origin.forward * 20f, Color.blue, 5f);*/
        Vector3 direction = player.position - origin.position;

        if (Physics.Raycast(origin.position, direction, out RaycastHit hit, 20f, layer))
        {
            Debug.Log("chocaste");
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Jugador");
                Debug.DrawRay(origin.position, direction, Color.green, 5f);
            }
            else
                Debug.DrawRay(origin.position, direction, Color.red, 5f);
        }
        else
            Debug.DrawRay(origin.position, direction, Color.blue, 5f);
    }
}
