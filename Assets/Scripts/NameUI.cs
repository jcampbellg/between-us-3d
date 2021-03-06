﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameUI : MonoBehaviour
{
    public GameState gameState;
    List<GameObject> playersList = new List<GameObject>();

    void Start()
    {
        playersList = new List<GameObject>(gameState.playersList);
    }

    void Update()
    {
        if (playersList != gameState.playersList)
        {
            RefreshUI();
        }
    }
    void RefreshUI()
    {
        playersList = new List<GameObject>(gameState.playersList);

        if (gameState.gameState == GameState.State.onLobby)
        {
            for (int i = 2; i < 14; i++)
            {
                GameObject playerName = this.transform.GetChild(i).gameObject;

                if (i - 2 < playersList.Count && playersList[i - 2] != null)
                {
                    playerName.SetActive(true);
                    TextMeshProUGUI text = playerName.GetComponent<TextMeshProUGUI>();
                    Image logo = playerName.transform.GetChild(0).GetComponent<Image>();

                    ClientController client = playersList[i - 2].GetComponent<ClientController>();

                    if (client.isLocalPlayer)
                        text.text = "[YOU] ";
                    else
                        text.text = "";

                    if (!client.isReady)
                        text.text += client.playerName;
                    else
                        text.text += client.playerName + " [READY]";

                    logo.sprite = playersList[i - 2].GetComponent<SkinRenderer>().skin.logo;
                }
                else
                {
                    playerName.SetActive(false);
                }
            }
        }
        else if (gameState.gameState == GameState.State.onMeeting)
		{
            for (int i = 2; i < 14; i++)
            {
                GameObject playerName = this.transform.GetChild(i).gameObject;

                if (i - 2 < playersList.Count && playersList[i - 2] != null)
                {
                    playerName.SetActive(true);
                    TextMeshProUGUI text = playerName.GetComponent<TextMeshProUGUI>();
                    Image logo = playerName.transform.GetChild(0).GetComponent<Image>();

                    ClientController client = playersList[i - 2].GetComponent<ClientController>();

                    if (client.isLocalPlayer)
                        text.text = "[Me] ";
                    else
                        text.text = "";

                    if (!client.isGhost)
					{
                        if (!client.hasVoted)
                            text.text += client.playerName;
                        else
                            text.text += client.playerName + " [I VOTED]";
                    }
					else
					{
                        text.text += client.playerName + " [DEAD]";
                    }

                    logo.sprite = playersList[i - 2].GetComponent<SkinRenderer>().skin.logo;
                }
                else
                {
                    playerName.SetActive(false);
                }
            }
        }
    }
}
