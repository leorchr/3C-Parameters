using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGL_Bofor : QuestNpc
{
    [SerializeField] private List<ItemData> _items = new List<ItemData>();
    [SerializeField] private Collider _anvilCollider, _chestCollider, _workbenchCollider;

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
            if (current == 0)
            {
                Inventory.Instance.AddToInventory(_items[0], 1);
                _anvilCollider.enabled = true;
            }
            if (current == 1)
            {
                _chestCollider.enabled = true;
            }
            if (current == 2)
            {
                Inventory.Instance.AddToInventory(_items[1], 1);
                Inventory.Instance.AddToInventory(_items[2], 1);
                _workbenchCollider.enabled = true;
            }
            QuestManager.Instance.TakeQuest(quests[current]);
        }
    }
}
