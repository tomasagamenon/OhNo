using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLimits : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisione con " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            Transform playerTransform = collision.gameObject.GetComponent<Transform>();
            //playerTransform.transform.Translate()
        }

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Colisione con " + hit.gameObject.name);
        if (hit.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            Transform playerTransform = hit.gameObject.GetComponent<Transform>();
            //playerTransform.transform.Translate()
        }
    }
}
