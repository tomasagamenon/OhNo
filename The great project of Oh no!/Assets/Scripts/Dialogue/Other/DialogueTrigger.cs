using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //public bool isActive;

    public Dialogue dialogue;
    private DialogueManager dM;

    public GameObject toActivate;
    public GameObject toDeactivate;

    private void Awake()
    {
        dM = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }
    
    public void TriggerDialogue()
    {
        //if(isActive)
        dM.StartDialogue(dialogue);
    }

    public void Interacted()
    {
        TriggerDialogue();
    }

    public void DialogueFinish()
    {
        if(toDeactivate != null)
        {
            toDeactivate.SetActive(false);
        }
        if(toActivate != null)
        {
            toActivate.SetActive(true);
        }
    }

    /*public void ChangeActive(bool active)
    {
        isActive = active;
    }*/
}
