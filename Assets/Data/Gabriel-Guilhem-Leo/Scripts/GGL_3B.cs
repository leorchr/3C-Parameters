using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGL_3B : QuestNpc
{
    public ItemData _data;
    public Collider npc,npc2,npc3;
    public Collider bofor;

    public override void GiveQuest()
    {
        if (quests.Count > 0 && current < quests.Count)
        {
            gaveQuest = true;
            waitForObject = true;
            //Setting up requirements to finish quests
            foreach (QuestItem item in quests[current].requirements)
            {
                requiredItems.Add(item);
            }
            if (current == 3)
            {
                Inventory.Instance.AddToInventory(_data,3);
                npc.enabled = true;
                npc2.enabled = true;
                npc3.enabled = true;
            }
            QuestManager.Instance.TakeQuest(quests[current]);
        }
    }

    public override void FinishQuest()
    {
        //Dialogue end quest
        foreach (QuestItem required in quests[current].requirements)
        {
            Inventory.Instance.RemoveFromInventory(required.item, required.quantity);
        }
        QuestManager.Instance.CompleteQuest(quests[current]);

        waitForObject = false;
        requiredItems.Clear();
        current++;
        gaveQuest = false;

        if (current == quests.Count)
        {
            Destroy(this);
            //bofor.enabled = true;
        }
    }
}