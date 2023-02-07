using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGL_Anvil : Interactive
{
    [SerializeField] private ItemData _blade;
    [SerializeField] private Collider _anvilCollider;
    [SerializeField] private string _thankYouMessage;

    public override void OnInteraction()
    {
        if (onlyOnce)
        {
            Inventory.Instance.RemoveFromInventory(requiredItems[0].item, 1);
            Inventory.Instance.AddToInventory(_blade);
            QuestManager.Instance.Notify();
            GGL_ThankYouUI.Instance.ThankYou(_thankYouMessage);
            _anvilCollider.enabled = false;
        }
    }
}
