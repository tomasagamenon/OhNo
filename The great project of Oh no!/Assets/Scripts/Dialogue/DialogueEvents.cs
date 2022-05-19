using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DialogueEvents : MonoBehaviour
{
    [SerializeField] private DialogueObject[] dialogue;
    [SerializeField] private Events[] events;
    public DialogueObject[] Dialogue => dialogue;
    public Events[] Events => events;

    public void OnValidate()
    {
        if (dialogue == null) return;
        if (events != null && events.Length == dialogue.Length) return;

        if (events == null)
        {
            events = new Events[dialogue.Length];
        }
        else
        {
            Array.Resize(ref events, dialogue.Length);
        }
    }
}
