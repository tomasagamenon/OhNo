using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text textName;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject nameBox;
    public GameObject DialogueBox => dialogueBox;
    public DialogueObject dialogue;
    private TypeEffect typeEffect;
    private ResponsesHandler responsesHandler;
    public GameObject player;
    public GameObject speaker;

    private bool look;
    private Vector3 pos;

    public float closeDialogueDistance;

    void Start()
    {
        typeEffect=GetComponent<TypeEffect>();
        responsesHandler=GetComponent<ResponsesHandler>();
    }

    void Update()
    {
        if(look)
        {
            Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - speaker.transform.position);
            lookRotation.x = speaker.transform.rotation.x;
            lookRotation.z = speaker.transform.rotation.z;
            speaker.transform.rotation = Quaternion.Slerp(speaker.transform.rotation, lookRotation, 10 * Time.deltaTime);
        }
        if (speaker != null)
            if (Vector3.Distance(speaker.transform.position, player.transform.position) > closeDialogueDistance)
                CloseDialogue();
    }

    public void ShowDialogue(DialogueObject dialogueObject, GameObject newSpeaker)
    {
        speaker = newSpeaker;
        dialogueBox.SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponentInChildren<PlayerCamera>().enabled = false;
        if (speaker.GetComponent<StreetNPC>())
        {
            pos = speaker.transform.position;
            look = true;
            speaker.GetComponent<NavMeshAgent>().isStopped = true;
            speaker.GetComponent<Animator>().SetBool("Idle", true);
        }
        player.GetComponentInChildren<PlayerDialogueVisual>().VisualFocus(speaker.transform.position);
        player.GetComponentInChildren<PlayerCameraDialogueVisual>().VisualFocus(speaker.GetComponent<StreetNPC>().face.transform.position);
        Cursor.lockState = CursorLockMode.None;
        textName.text = "<b>" + speaker.name + "</b>";
        for (int i = 0; i <= textName.text.Length; i++)
        {
            nameBox.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, i * 31);
        }
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typeEffect.Run(dialogue, text);
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            yield return new WaitUntil(() => Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape));
        }

        if(dialogueObject.HasResponses)
        {
            responsesHandler.ShowResponses(dialogueObject.Responses, speaker);
        }
        else
        {
            if(speaker.GetComponent<DialogueEvents>())
            {
                var a = speaker.GetComponent<DialogueEvents>();
                for (int i = 0; i < a.Dialogue.Length; i++)
                {
                    if(dialogueObject == a.Dialogue[i])
                        a.Events[i].DialogueEvent.Invoke();
                }
            }
            CloseDialogue();
        }
    }

    public void CloseDialogue()
    {
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponentInChildren<PlayerCamera>().enabled = true;
        if (speaker.GetComponent<StreetNPC>())
        {
            if (speaker.GetComponent<NavMeshAgent>().enabled == false)
                speaker.GetComponent<NavMeshAgent>().enabled = true;
            speaker.GetComponent<NavMeshAgent>().isStopped = false;
            speaker.GetComponent<NavMeshAgent>().SetDestination(speaker.GetComponent<StreetNPC>().end.position);
            look = false;
            pos = Vector3.zero;
            speaker.GetComponent<Animator>().SetBool("Idle", false);
        }
        player.GetComponent<PlayerDialogueVisual>().DeFocus();
        player.GetComponentInChildren<PlayerCameraDialogueVisual>().DeFocus();
        Cursor.lockState = CursorLockMode.Locked;
        dialogueBox.SetActive(false);
        text.text = string.Empty;
        textName.text = string.Empty;
        speaker.GetComponent<InteractiveObject>().canInteract = true;
        speaker = null;
    }
}
