using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

[CustomEditor(typeof(AchievementUI))]
public class AchievementsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //base.OnInspectorGUI();
          
        AchievementUI achUI = (AchievementUI)target;
        GUILayout.Space(25);
        if (GUILayout.Button("Check Achievements", GUILayout.MaxWidth(300)))
            achUI.CheckAchievements();
        if (GUILayout.Button("Create Achievements List", GUILayout.MaxWidth(300)))
            achUI.CreateList();
        if (GUILayout.Button("Reset Achievements List", GUILayout.MaxWidth(300)))
            achUI.ResetList();
        /*if(GUILayout.Button("Create Achievement Card", GUILayout.MaxWidth(300)))
            achUI.CreateCard();*/
    }
}
