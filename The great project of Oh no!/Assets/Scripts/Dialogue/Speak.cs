using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Speak : MonoBehaviour
{
    [SerializeField] private bool randomize;
    public int _count;
    public DialogueUI textUI;
    public List<DialogueObject> dialogue;
    private bool blockDialogue;
    [SerializeField] private float crazyInteractionsProb;
    [SerializeField] private List<string> crazyInteractions;
    private string crazyInteraction;

    public void Interact()
    {
        for(int i = 0; i < dialogue.Count; i++)
        {
            if (dialogue[i].ObjectThatINeed != "")
            {
                if (FindObjectOfType<Inventory>().IHaveThis(dialogue[i]))
                {
                    blockDialogue = true;
                    _count = i;
                }
                else blockDialogue = false;
            }
        }
        if (randomize && blockDialogue == false)
            _count = UnityEngine.Random.Range(0, dialogue.Count);
        if (dialogue[_count].isAccesible && FindObjectOfType<Inventory>().IHaveThis(dialogue[_count]))
        {
            textUI.gameObject.SetActive(true);
            textUI.speaker = this.gameObject;
            textUI.ShowDialogue(dialogue[_count], gameObject);
            GetComponent<InteractiveObject>().canInteract = false;
            if (_count < dialogue.Count - 1 && blockDialogue == false)
            {
                if (dialogue[_count + 1].isAccesible)
                    _count++;
                else _count += 2;
            }
            if (_count > dialogue.Count && blockDialogue == false)
                _count = dialogue.Count;
        }
        var prob = UnityEngine.Random.Range(0, crazyInteractionsProb);
        if ((100 - crazyInteractionsProb) - prob < 0)
        {
            crazyInteraction = crazyInteractions[UnityEngine.Random.Range(0, crazyInteractions.Count)];
            StartCoroutine(crazyInteraction);
        }
        else crazyInteraction = "";
    }

    IEnumerator TPose()
    {
        GetComponent<Animator>().SetBool(crazyInteraction, true);
        yield return new WaitUntil(() => textUI.speaker == null);
        GetComponent<Animator>().SetBool(crazyInteraction, false);
    }
    IEnumerator Up()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        yield return new WaitUntil(() => textUI.speaker == null);
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
    }
    IEnumerator WalkBackwards()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<Animator>().SetBool("Idle", false);
        GetComponent<NavMeshAgent>().isStopped = false;
    }

    public void Accsesible(DialogueObject dialogueO)
    {
        dialogueO.isAccesible = true;
    }

    public void RemoveDialogue(DialogueObject dialogueObject)
    {
        dialogue.Remove(dialogueObject);
    }
}
