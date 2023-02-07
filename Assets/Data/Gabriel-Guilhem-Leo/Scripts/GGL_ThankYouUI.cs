using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GGL_ThankYouUI : MonoBehaviour
{
    [SerializeField] private GameObject thankYouPanel;
    [SerializeField] private TextMeshProUGUI thankYou;
    [SerializeField] private Button welcome;

    public static GGL_ThankYouUI Instance;

    void Start()
    {
        if (Instance) Destroy(this);
        else Instance = this;

        welcome.onClick.AddListener(delegate
        {
            thankYouPanel.SetActive(false);
            Time.timeScale = 1;
        });
    }

    public void ThankYou(string thankYouMessage)
    {
        thankYou.text = thankYouMessage;
        thankYouPanel.SetActive(true);
        Time.timeScale = 0;
        welcome.Select();
    }
}
