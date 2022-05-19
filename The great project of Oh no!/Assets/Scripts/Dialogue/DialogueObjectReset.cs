using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObjectReset : MonoBehaviour
{
    [SerializeField] private DialogueObject[] dialogueObjects;
    void Start()
    {
        foreach (DialogueObject dialogue in dialogueObjects)
            dialogue.isAccesible = dialogue.IsAccesibleRestart;
    }
}
