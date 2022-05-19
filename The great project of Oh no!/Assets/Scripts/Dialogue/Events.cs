using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Events
{
    [SerializeField] private UnityEvent dialogueEvent;
    public UnityEvent DialogueEvent => dialogueEvent;
}
