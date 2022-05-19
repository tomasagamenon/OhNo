using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public float letterSpeed;

    public Animator animator;
    
    private Queue<string> _sentences;

    public GameObject player;
    //private MonoBehaviour _nextAction;
    private DialogueTrigger _actualTrigger;
    //private bool isFinal;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(player != null)
        {
            if(dialogue.visualPoint != null)
            {
                player.GetComponent<PlayerDialogueVisual>().VisualFocus(dialogue.visualPoint.position);
            }
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<Gun>().enabled = false;
            player.GetComponentInChildren<PlayerCamera>().enabled = false;
        }

        //isFinal = dialogue.isFinal;
        _actualTrigger = dialogue.actualTrigger;

        /*if (dialogue.nextAction != null)
        {
            Debug.Log("se detecta siguiente acción " + dialogue.nextAction);
            _nextAction = dialogue.nextAction;
        }*/
        
        Cursor.lockState = CursorLockMode.Confined;

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        _sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if(_sentences.Count == 0)
        {
            if(player != null)
            {
                player.GetComponent<PlayerDialogueVisual>().DeFocus();
                player.GetComponent<PlayerController>().enabled = true;
                player.GetComponent<Gun>().enabled = true;
                player.GetComponentInChildren<PlayerCamera>().enabled = true;
                
            }
            /*if(_nextAction != null)
            {
                Debug.Log("se desactiva el actual y se activa el siguiente");
                _actualTrigger.enabled = false;
                _nextAction.enabled = true;
            }else if (isFinal)
            {
                _actualTrigger.enabled = false;
                Debug.Log("se desactiva el final");
            }
            _nextAction = null;*/
            

            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterSpeed);
        }
    }

    public void EndDialogue()
    {
        _actualTrigger.DialogueFinish();
        animator.SetBool("IsOpen", false);
        //Debug.Log("Your mind is now silent");
    }
}
