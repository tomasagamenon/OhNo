using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Objects/DialogueObject")]

public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    [SerializeField] private UnityEvent dialogueEvent;
    [SerializeField] private string objectThatINeed;
    [SerializeField] private bool isAccesibleRestart = true;
    public UnityEvent DialogueEvent => dialogueEvent;

    public string[] Dialogue => dialogue;
    public Response[] Responses => responses;
    public string ObjectThatINeed => objectThatINeed;
    public bool HasResponses => Responses != null && Responses.Length > 0;
    public bool IsAccesibleRestart => isAccesibleRestart;

    public bool isAccesible = true;
}
