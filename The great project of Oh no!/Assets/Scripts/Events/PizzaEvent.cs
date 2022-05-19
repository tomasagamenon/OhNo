using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaEvent : MonoBehaviour
{
    public float rotationSpeed;
    private bool look;
    private Transform player;
    private Quaternion rot;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    private void Update()
    {
        if(look)
        {
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponentInChildren<PlayerCamera>().enabled = false;
            Vector3 direction = transform.position - player.position;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            player.rotation = Quaternion.Slerp(player.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            rot = lookRotation;
        }
    }

    public void Mafia()
    {
        StartCoroutine(aa());
    }
    IEnumerator aa()
    {
        yield return new WaitForSeconds(0.1f);
        look = true;
        yield return new WaitUntil(()=> player.rotation == rot);
        look = false;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponentInChildren<PlayerCamera>().enabled = true;
        if (GetComponent<InteractiveObject>())
            GetComponent<InteractiveObject>().Interact();
    }
}
