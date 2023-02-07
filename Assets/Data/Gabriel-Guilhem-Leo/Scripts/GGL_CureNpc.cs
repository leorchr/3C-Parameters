using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GGL_CureNpc : Interactive
{
    public Collider interactionCollider;
    public ItemData _validCure;
    [SerializeField] private string thankYouMessage;
    public override void OnInteraction()
    {
        if (Inventory.Instance.IsItemFound(requiredItems[0].item))
        {
            if (onlyOnce)
            {
                Inventory.Instance.RemoveFromInventory(requiredItems[0].item, 1);
                Inventory.Instance.AddToInventory(_validCure);
                QuestManager.Instance.Notify();
                interactionCollider.enabled = false;
                GGL_ThankYouUI.Instance.ThankYou(thankYouMessage);
            }
        }
    }
}
