using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameUI : MonoBehaviour
{
    public Settings settings;
    List<GameObject> playersList = new List<GameObject>();

    void Start()
    {
        playersList = new List<GameObject>(settings.playersList);
    }

    void Update()
    {
        if (playersList != settings.playersList)
        {
            RefreshUI();
        }
    }
    void RefreshUI()
    {
        playersList = new List<GameObject>(settings.playersList);

        for (int i = 1; i < 13; i++)
        {
            GameObject playerName = this.transform.GetChild(i).gameObject;

            if (i-1 < playersList.Count && playersList[i-1] != null)
            {
                playerName.SetActive(true);
                TextMeshProUGUI text = playerName.GetComponent<TextMeshProUGUI>();
                Image logo = playerName.transform.GetChild(0).GetComponent<Image>();

                ClientController client = playersList[i-1].GetComponent<ClientController>();

                if (!client.isReady)
                    text.text = client.playerName;
                else
                    text.text = client.playerName + " [READY]";

                logo.sprite = playersList[i-1].GetComponent<SkinRenderer>().skin.logo;
            }
            else
            {
                playerName.SetActive(false);
            }
        }
    }
}
