using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public TextMeshProUGUI title, description, progress;
    public Sprite completeQuest;
    private QuestData trackedQuest;
    private int max = 0;

    public void SetupQuest(QuestData quest)
    {
        trackedQuest = quest;
        title.text = quest.title;
        SetTotalRequirements();
        description.text = quest.shortDescription;
        Notify();
    }

    public void Notify()
    {
        int amount = 0;
        foreach (QuestItem item in trackedQuest.requirements)
        {
            int qtt = Inventory.Instance.GetItemQuantity(item);
            amount += qtt;
        }
        progress.text = amount +" / " + max;
        if (Inventory.Instance.HasEveryItem(trackedQuest.requirements))
        {
            Complete();
        }
    }

    public void SetTotalRequirements()
    {
        foreach (QuestItem item in trackedQuest.requirements)
        {
            max += item.quantity;
        }
    }

    public void Complete()
    {
        GetComponent<Image>().sprite = completeQuest;
    }
}
