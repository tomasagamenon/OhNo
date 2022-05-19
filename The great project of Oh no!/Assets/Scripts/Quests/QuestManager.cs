using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] quests;
    public QuestUI questUI;
    public AchievementUI achievementUI;
    private int _activeQuests;
    private int _visibleQuests;
    private void Awake()
    {
        if (gameObject.CompareTag("QuestManager"))
        {
            for(int i = 0; i < quests.Length; i++)
            {
                Quest quest = quests[i];
                quest.Reset();
                quest.qm = gameObject.GetComponent<QuestManager>();

                if (quest.isActive)
                {
                    if (quest.isVisible)
                    {
                        quest.visibleInt = _visibleQuests;
                        DefineQuests(quest);
                        _visibleQuests++;
                    }
                    _activeQuests++;
                }
            }
        }
    }
    public void ActivateQuest(Quest quest)
    {
        //Debug.Log("se va a activar la mision " + quest.questName);
        for(int i = 0; i < quests.Length; i++)
        {
            Quest aQuest = quests[i];
            if(aQuest == quest && !quest.isActive)
            {
                aQuest.isActive = true;
                if (aQuest.isVisible)
                {
                    aQuest.visibleInt = _visibleQuests;
                    SortQuests();
                }
            }
        }
    }
    public void AddAmount(string name, int amount, Quest objectiveQuest)
    {
        for (int i = 0; i < quests.Length; i++)
        {
            Quest quest = quests[i];
            if(quest == objectiveQuest && quest.isActive && !quest.isWon && !quest.isLost)
            {
                quest.AddAmount(name, amount);

                if (quest.Success())
                {
                    SendMessage("QuestWin", quest, SendMessageOptions.DontRequireReceiver);
                    if (gameObject.CompareTag("QuestManager") && quest.isVisible)
                        DiscardQuest(quest);
                    else if (gameObject.CompareTag("AchievementManager"))
                        achievementUI.CompleteCard(quest);
                }
                if (quest.Lose())
                {
                    SendMessage("QuestLose", quest, SendMessageOptions.DontRequireReceiver);
                    if (quest.isVisible)
                        DiscardQuest(quest);
                    //Debug.Log("CONFIRMACION de derrota en " + quest.questName);
                }
            }
        }
    }

    public void EqualAmount(string name, int amount)
    {
        for (int i = 0; i < quests.Length; i++)
        {
            Quest quest = quests[i];
            if(quest.isActive && !quest.isWon && !quest.isLost)
            {
                quest.EqualAmount(name, amount);

                if (quest.Success())
                {
                    SendMessage("QuestWin", quest, SendMessageOptions.DontRequireReceiver);
                    if (gameObject.CompareTag("QuestManager") && quest.isVisible)
                        DiscardQuest(quest);
                    else if (gameObject.CompareTag("AchievementManager"))
                        achievementUI.CompleteCard(quest);
                }

                if (quest.Lose())
                {
                    SendMessage("QuestLose", quest, SendMessageOptions.DontRequireReceiver);
                    if (quest.isVisible)
                        DiscardQuest(quest);
                }
            }
        }
    }

    public void SortQuests()
    {
        _visibleQuests = 0;
        _activeQuests = 0;
        for(int i = 0; i < quests.Length; i++)
        {
            Quest quest = quests[i];
            if (quest.isActive)
            {
                if (quest.isVisible)
                {
                    DefineQuests(quest);
                    _visibleQuests++;
                }
                _activeQuests++;
            }
        }
    }
    public void DefineQuests(Quest quest)
    {
        //Debug.Log("la mision " + quest.questName + " tiene la tarjeta n " + quest.visibleInt);
        //Debug.Log("la mision " + quest.name + " esta activa y visible");
        List<string> winConds = new List<string>();
        List<string> loseConds = new List<string>();
        List<string> compConds = new List<string>();
        if (quest.winConditions.Length != 0)
            for (int ii = 0; ii < quest.winConditions.Length; ii++)
            {
                if (quest.winConditions[ii].isActive)
                {
                    if (quest.winConditions[ii].isComplete)
                        compConds.Add(quest.winConditions[ii].description);
                    else
                        winConds.Add(quest.winConditions[ii].description);
                }
            }
        if (quest.loseConditions.Length != 0)
            for (int iii = 0; iii < quest.loseConditions.Length; iii++)
            {
                loseConds.Add(quest.loseConditions[iii].description);
            }
        questUI.SetQuests(quest.questName, quest.questDescription, winConds, loseConds, compConds, quest.visibleInt);
    }

    public void DiscardQuest(Quest quest)
    {
        //Debug.Log("se inicia la limpieza");
        for(int i = 0; i < quests.Length; i++)
        {
            if (quests[i].visibleInt > quest.visibleInt)
                quests[i].visibleInt--;
        }
        quest.visibleInt = -1;
        questUI.DeleteQuest();
        SortQuests();
    }
}
