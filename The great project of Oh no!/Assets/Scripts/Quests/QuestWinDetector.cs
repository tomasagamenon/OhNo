using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestWinDetector : MonoBehaviour
{
    public QuestToDetect[] questsToCheck;
    //private CosaQueUseGuita money;
    public void QuestWin(Quest wonQuest)
    { 
        for(int i = 0; i < questsToCheck.Length; i++)
        {
            Quest questToCheck = questsToCheck[i].questToDetect;
            if (questToCheck == wonQuest)
            {
                Debug.Log("Misión " + questToCheck.name + " se gano y aca esta la recompensa");
                QuestToDetect values = questsToCheck[i];
                if(values.moneyReward != 0)
                {
                    //aca va la suma de guita cuando haya otro script para eso
                    Debug.Log("ganaste " + values.moneyReward + " pesos");
                }
                if(values.activateOnWin != null)
                    values.activateOnWin.isActive = true;
                if (values.toActivate != null) 
                    values.toActivate.SetActive(true);
                if (values.toDeActivate != null) 
                    values.toDeActivate.SetActive(false);
            }
        }
    }

    public void QuestLose(Quest lostQuest)
    {
        for(int i = 0; i < questsToCheck.Length; i++)
        {
            Quest questToCheck = questsToCheck[i].questToDetect;
            if (questToCheck == lostQuest)
            {
                Debug.Log("Mision " + questToCheck.name + " se perdio y aca esta la consecuencia");
                QuestToDetect values = questsToCheck[i];
                if (values.activateOnLose != null)
                    values.activateOnLose.isActive = true;
            }
        }
    }
}
