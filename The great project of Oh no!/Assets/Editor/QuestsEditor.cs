using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestGeneric))]
public class QuestsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        QuestGeneric generic = (QuestGeneric)target;

        if (generic.questType == QuestGeneric.QuestType.Achievement)
            generic.manager = GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<QuestManager>();
        else if (generic.questType == QuestGeneric.QuestType.Quest)
            generic.manager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
    }
}
