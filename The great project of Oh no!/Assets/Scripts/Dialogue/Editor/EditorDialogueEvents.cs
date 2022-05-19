using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueEvents))]
public class EditorDialogueEvents : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueEvents dialogueEvents = (DialogueEvents)target;

        if (GUILayout.Button("Refresh"))
            dialogueEvents.OnValidate();
    }
}
