using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGL_Chest: Interactive
{
    [SerializeField] private ItemData _handle;
    [SerializeField] private Collider _chestCollider;
    [SerializeField] private GameObject _chestClose, _chestOpen;

    public override void OnInteraction()
    {
        if (onlyOnce)
        {
            _chestClose.SetActive(false);
            _chestOpen.SetActive(true);
            Inventory.Instance.AddToInventory(_handle);
            QuestManager.Instance.Notify();
            _chestCollider.enabled = false;
        }
    }
}
