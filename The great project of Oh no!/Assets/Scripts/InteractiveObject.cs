using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject[] reactions;
    public bool canInteract = true;

    public void Interact()
    {
        //reaction.SendMessage("Interacted");
        //SendMessage("Interacted", SendMessageOptions.DontRequireReceiver);
        /*for (int i = 0; i < reactions.Length; i++)
        {
            reactions[i].SendMessage("Interacted", SendMessageOptions.DontRequireReceiver);
        }*/
        Debug.Log("Interactuaste con " + gameObject.name);
        if (GetComponent<Speak>())
            GetComponent<Speak>().Interact();
        if (GetComponent<Item>())
            GetComponent<Item>().Get();
    }
}
