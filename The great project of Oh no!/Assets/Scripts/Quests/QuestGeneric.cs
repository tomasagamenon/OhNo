using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGeneric : MonoBehaviour
{
    public QuestManager manager;
    public Quest quest;
    public enum ChangeType { Minus, Plus, Equal }
    public enum QuestType { Achievement, Quest}

    public string conditionName;
    public int value;
    public ChangeType changeType;
    public QuestType questType;

    public void DoIt()
    {
        if(changeType == ChangeType.Minus)
        {
            manager.AddAmount(conditionName, -value, quest);
        }else if(changeType == ChangeType.Plus)
        {
            manager.AddAmount(conditionName, value, quest);
        }else if(changeType == ChangeType.Equal)
        {
            manager.EqualAmount(conditionName, value);
        }
    }

    public void Interacted()
    {
        Debug.Log("interactuaste con QuestGeneric en " + gameObject.name);
        DoIt();
    }
}
