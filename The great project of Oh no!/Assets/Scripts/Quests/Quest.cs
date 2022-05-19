using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : ScriptableObject
{
    public string questName;
    [TextArea(1,3)]public string questDescription;
    public bool isActive;
    public bool isVisible;
    public int visibleInt;
    public bool activeDefault;
    public bool isConsecutive;
    public bool isWon;
    public bool isLost;
    public QuestCondition[] winConditions;
    public QuestCondition[] loseConditions;

    public Sprite incompleteImage;
    public Sprite completeImage;
    public string alternativeName;
    [TextArea(1,3)]public string alternativeDescription;
    
    [HideInInspector] public QuestManager qm;

    public void AddAmount(string name, int amount)
    {
        for (int i = 0; i < winConditions.Length; i++)
        {
            QuestCondition winCondition = winConditions[i];
            if (isConsecutive)
            {
                if (winCondition.name == name && winCondition.isActive && !winCondition.isComplete)
                {
                    winCondition.currentAmount += amount;
                    //Debug.Log("se añadio " + amount + " a la condición de victoria " + name + " en la mision " + questName);
                    if (winCondition.Success())
                    {
                        winCondition.isComplete = true;
                        //Debug.Log("se cumplio la condicion " + winCondition.name + " de la mision " + questName);
                        if (i + 1 < winConditions.Length)
                        {
                            QuestCondition nextCondition = winConditions[i + 1];
                            nextCondition.isActive = true;
                            //Debug.Log("se activo la condicion " + nextCondition.name + " de la mision " + questName);
                        }
                        qm.SendMessage("SortQuests", this);
                    }
                    break;
                }
            }else if(winCondition.name == name && !winCondition.isComplete)
            {
                winCondition.currentAmount += amount;
                if (winCondition.Success())
                {
                    winCondition.isComplete = true;
                }
                //Debug.Log("se añadio " + amount + " a la condición de victoria " + name + " en la mision " + questName);
            }
            
        }

        for (int i = 0; i < loseConditions.Length; i++)
        {
            QuestCondition loseCondition = loseConditions[i];
            if (isConsecutive)
            {
                if (loseCondition.name == name && loseCondition.isActive && !loseCondition.isComplete)
                {
                    loseCondition.currentAmount += amount;
                    //Debug.Log("se añadio " + amount + " a la condicion de derrota " + name + " en la mision " + questName);
                    break;
                }
            }else if(loseCondition.name == name && !loseCondition.isComplete)
            {
                loseCondition.currentAmount += amount;
                //Debug.Log("se añadio " + amount + " a la condicion de derrota " + name + " en la mision " + questName);
            }
            
        }
    }

    public void EqualAmount(string name, int amount)
    {
        for (int i = 0; i < winConditions.Length; i++)
        {
            QuestCondition winCondition = winConditions[i];
            if (isConsecutive)
            {
                if (winCondition.name == name && winCondition.isActive && !winCondition.isComplete)
                {
                    winCondition.currentAmount = amount;
                    //Debug.Log("se igualo a " + amount + " la condición de victoria " + name + " en la mision " + questName);
                    if (winCondition.Success())
                    {
                        winCondition.isComplete = true;
                        //sendmessage
                        //Debug.Log("se cumplio la condicion " + winCondition.name + " de la mision " + questName);
                        if(i+1 < winConditions.Length)
                        {
                            QuestCondition nextCondition = winConditions[i + 1];
                            nextCondition.isActive = true;
                            //Debug.Log("se activo la condicion " + nextCondition.name + " de la mision " + questName);
                        }
                        qm.SendMessage("SortQuests", this);
                    }
                    break;
                }
            } else if(winCondition.name == name && !winCondition.isComplete)
            {
                winCondition.currentAmount = amount;
                if (winCondition.Success())
                {
                    winCondition.isComplete = true;
                    //sendmessage
                }
                //Debug.Log("se igualo a " + amount + " la condición de victoria " + name + " en la mision " + questName);
            }
            
        }

        for (int i = 0; i < loseConditions.Length; i++)
        {
            QuestCondition loseCondition = loseConditions[i];
            if (isConsecutive)
            {
                if (loseCondition.name == name && loseCondition.isActive && !loseCondition.isComplete)
                {
                    loseCondition.currentAmount = amount;
                    //Debug.Log("se igualo a " + amount + " la condicion de derrota " + name + " en la mision " + questName);
                    break;
                }
            } else if(loseCondition.name == name && !loseCondition.isComplete)
            {
                loseCondition.currentAmount = amount;
                //Debug.Log("se igualo a " + amount + " la condicion de derrota " + name + " en la mision " + questName);
            }
            
        }
    }

    public bool Success()
    {
        if(winConditions.Length != 0)
        {
            for (int i = 0; i < winConditions.Length; i++)
            {
                QuestCondition condition = winConditions[i];
                if (!condition.Success())
                {
                    //Debug.Log("PRUEBA SUCCESS FALSE " + condition.name + " en mision " + questName);
                    return false;
                }
            }
            //Debug.Log("se consiguio la mision " + questName);
            isWon = true;
            isActive = false;
            return true;
        }
        //Debug.Log("no se encontro ninguna condicion de victoria en la mision " + questName);
        return false;
    }

    public bool Lose()
    {
        if(loseConditions.Length != 0)
        {
            for (int i = 0; i < loseConditions.Length; i++)
            {
                QuestCondition condition = loseConditions[i];
                if (!condition.Success())
                {
                    //Debug.Log("PRUEBA LOSE FALSE " + condition.name + " en mision " + questName);
                    return false;
                }
            }
            //Debug.Log("se perdio la mision " + questName);
            isLost = true;
            isActive = false;
            return true;
        }
        //Debug.Log("no se detecto ninguna condicion de derrota en la mision " + questName);
        return false;
    }

    public void Reset()
    {
        isWon = false;
        isLost = false;
        visibleInt = 0;
        if (activeDefault) isActive = true;
        else isActive = false;
        for(int i = 0; i < winConditions.Length; i++)
        {
            QuestCondition condition = winConditions[i];
            if(condition.resetAmount != -314)
            {
                condition.currentAmount = condition.resetAmount;
            }
        }
        for (int i = 0; i < loseConditions.Length; i++)
        {
            QuestCondition condition = loseConditions[i];
            if (condition.resetAmount != -314)
            {
                condition.currentAmount = condition.resetAmount;
            }
        }
        if (isConsecutive)
        {
            for(int i = 0; i < winConditions.Length; i++)
            {
                QuestCondition winCondition = winConditions[i];
                winCondition.isComplete = false;
                if (i == 0 && isActive)
                    winCondition.isActive = true;
                else 
                    winCondition.isActive = false;
            }
            for (int i = 0; i < loseConditions.Length; i++)
            {
                QuestCondition loseCondition = loseConditions[i];
                loseCondition.isComplete = false;
                if (i == 0 && isActive)
                    loseCondition.isActive = true;
                else
                    loseCondition.isActive = false;
            }
        } else if (!isConsecutive)
        {
            for (int i = 0; i < winConditions.Length; i++)
            {
                QuestCondition winCondition = winConditions[i];
                winCondition.isComplete = false;
                winCondition.isActive = true;
            }
            for (int i = 0; i < loseConditions.Length; i++)
            {
                QuestCondition loseCondition = loseConditions[i];
                loseCondition.isComplete = false;
                loseCondition.isActive = true;
            }
        }
    }
}
