using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResponsesHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;
    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start()
    {
        dialogueUI=GetComponent<DialogueUI>();
    }

    public void ShowResponses(Response[] responses, GameObject speaker)
    {
        float responseBoxHeight = 0;
        int responsesActivate = 0;
        foreach(Response response in responses)
        {
            if (response.DialogueObject.ObjectThatINeed != "")
            {
                if (FindObjectOfType<Inventory>().IHaveThis(response.DialogueObject))
                {
                    if (response.DialogueObject.isAccesible)
                    {
                        GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
                        responseButton.gameObject.SetActive(true);
                        responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
                        responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, speaker));
                        tempResponseButtons.Add(responseButton);
                        responseBoxHeight += responseButtonTemplate.sizeDelta.y;
                        Debug.Log(responseButtonTemplate.sizeDelta.y);
                    }
                    responsesActivate++;
                }
            }
            else
            {
                if (response.DialogueObject.isAccesible)
                {
                    GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
                    responseButton.gameObject.SetActive(true);
                    responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
                    responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, speaker));
                    tempResponseButtons.Add(responseButton);
                    responseBoxHeight += responseButtonTemplate.sizeDelta.y;
                    Debug.Log(responseButtonTemplate.sizeDelta.y);
                }
                responsesActivate++;
            }
        }
        if (responsesActivate <= 0)
        {
            dialogueUI.CloseDialogue();
            return;
        }
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }


    private void OnPickedResponse(Response response, GameObject speaker)
    {
        responseBox.gameObject.SetActive(false);
        foreach (GameObject button in tempResponseButtons)
            Destroy(button);
        dialogueUI.ShowDialogue(response.DialogueObject, speaker);
    }
}
