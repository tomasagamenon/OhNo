using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public QuestCards[] questCards;

    public void SetQuests(string name, string desc, List<string> winConds, List<string> loseConds, List<string> completeConds, int visibleQuest)
    {
        QuestCards questCard = questCards[visibleQuest];
        if (!questCard.questCard.activeInHierarchy)
            questCard.questCard.SetActive(true);
        questCard.questName.text = name;
        questCard.questDesc.text = desc;
        questCard.questCond.text = "Do:\n";
        //Debug.Log(winConds.Count);
        if (completeConds.Count != 0)
        {
            for(int compInt = 0; compInt < completeConds.Count; compInt++)
                questCard.questCond.text += "- <indent=10%><s>" + completeConds[compInt] + "</s></indent=10%>\n";
        }
        for (int ii = 0; ii < winConds.Count; ii++)
        {
            questCard.questCond.text += "- <indent=10%>" + winConds[ii] + "</indent=10%>\n";
        }
        if(loseConds.Count != 0)
        {
            questCard.questCond.text += "Don't:\n";

            for (int iii = 0; iii < loseConds.Count; iii++)
                questCard.questCond.text += "- <indent=10%>" + loseConds[iii] + "</indent=10%>\n";
        }
    }

    public void DeleteQuest()
    {
        for (int i = 0; i < questCards.Length; i++)
        {
            QuestCards questCard = questCards[i];
            questCard.questCond.text = "";
            questCard.questDesc.text = "";
            questCard.questName.text = "";
            questCard.questCard.SetActive(false);
        }
        //Debug.Log("se termina la limpieza");
    }
}
