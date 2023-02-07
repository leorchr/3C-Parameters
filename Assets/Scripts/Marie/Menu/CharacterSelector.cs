using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public List<GameObject> characters;
    void Awake()
    {
        int playerChoice = 0;
        if (PlayerPrefs.HasKey("Character"))
        {
            playerChoice = PlayerPrefs.GetInt("Character");
        }

        characters[playerChoice].SetActive(true);
        characters[(playerChoice+1)%2].SetActive(false);
    }
}
