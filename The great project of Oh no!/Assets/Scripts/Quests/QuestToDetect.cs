using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class QuestToDetect
{
    public Quest questToDetect;
    public int moneyReward;
    public GameObject toActivate;
    public GameObject toDeActivate;
    public Quest activateOnWin;
    public Quest activateOnLose;
}
