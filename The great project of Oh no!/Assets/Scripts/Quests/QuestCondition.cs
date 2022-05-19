using System;
using UnityEngine;

[Serializable]
public class QuestCondition
{
    public enum ConditionType { Less, Greater, Equal }

    public string name;
    [TextArea(2,5)]public string description;
    public int currentAmount;
    public int targetAmount;
    public int resetAmount;
    public ConditionType type;
    public bool isActive;
    public bool isComplete;

    public bool Success()
    {
        return (type == ConditionType.Equal && currentAmount == targetAmount) ||
               (type == ConditionType.Less && currentAmount < targetAmount) ||
               (type == ConditionType.Greater && currentAmount > targetAmount);
    }
}
