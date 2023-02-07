using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGL_Workbench: Interactive
{
    [SerializeField] private ItemData _legendaryBlade;
    [SerializeField] private Collider _workbenchCollider;
    [SerializeField] private string _thankYouMessage;

    public override void OnInteraction()
    {
        if (onlyOnce)
        {
            Inventory.Instance.RemoveFromInventory(requiredItems[0].item, 1);
            Inventory.Instance.RemoveFromInventory(requiredItems[1].item, 1);
            Inventory.Instance.AddToInventory(_legendaryBlade);
            QuestManager.Instance.Notify();
            GGL_ThankYouUI.Instance.ThankYou(_thankYouMessage);
            _workbenchCollider.enabled = false;
        }
    }
}
