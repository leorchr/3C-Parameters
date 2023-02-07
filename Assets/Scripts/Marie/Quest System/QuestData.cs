using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    public string title, shortDescription;
    [TextArea] public string description;
    [TextArea] public string thankYouMessage;

    [TextArea] public string reward;

    public List<QuestItem> requirements = new List<QuestItem>();

    [Header("Rewards")]
    public int moneyReward;
    public QuestItem itemReward;
}


