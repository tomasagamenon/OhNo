using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3,10)]
    public string[] sentences;
    //public MonoBehaviour nextAction;
    public DialogueTrigger actualTrigger;
    public Transform visualPoint;
    //public bool isFinal;
}
