﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

[System.Serializable]
public class SyncListPlayers : SyncList<GameObject> { };

public class Settings : NetworkBehaviour
{
    public enum Setting
	{
        fogDensity,
        playerSpeed,
        killDistance,
        impostorsCount
    };
    public enum GameState
    {
        onLobby,
        onGame,
        onMeeting
    };
    public float mouseSensitivity;
    public bool isMenuOpen;
    public bool isPauseOpen;
    public bool isTaskOpen;
    public bool isMapOpen;

    public List<GameObject> playersList = new List<GameObject>();
    public List<GameObject> impostorsList = new List<GameObject>();
    public GameObject namesUI;
    public GameObject impostorsUI;
    public GameObject crewUI;
    public GameObject localPlayer;

    [SyncVar(hook = nameof(OnGameStart))]
    public GameState gameState = GameState.onLobby;

    [SyncVar(hook = nameof(HookChangeFog))]
    public float fogDensity = 0.09f;
    [SyncVar]
    public float clickPlayerDistance = 17.0f;
    [SyncVar]
    public float playerSpeed = 10.0f;
    [SyncVar]
    public float killDistance = 0.5f;
    [SyncVar]
    public int impostorsCount = 1;

    public void AddPlayersList(GameObject player)
	{
        playersList.Add(player);
	}
    public void RemovePlayersList(GameObject player)
    {
        if (isServer)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<BetweenUsNetworkManager>().skins.Add(player.GetComponent<SkinRenderer>().skin);
        }
        playersList.Remove(player);
    }

    public void Restart()
	{
        ChangeFog(0.09f);
        playerSpeed = 10.0f;
        killDistance = 0.5f;
        impostorsCount = 1;
    }
    public void HookChangeFog(float oldFog, float newFog)
	{
        ChangeFog(newFog);
	}
    public void ChangeFog(float newFog)
	{
        fogDensity = newFog;
        RenderSettings.fogDensity = newFog;
        clickPlayerDistance = ((0.4f - newFog) / 0.4f * 13f)+2.5f;
    }
    public void UpdateSettings(float _fogDistance, float _playerSpeed, float _killDistance)
	{
        ChangeFog(_fogDistance);
        playerSpeed = _playerSpeed;
        killDistance = _killDistance;
    }

    public void OnGameStart(GameState oldState, GameState newState)
	{
        if (oldState == GameState.onLobby && newState == GameState.onGame)
		{
            namesUI.SetActive(false);
        }
    }
    public void OpenRoleUI(GameObject player)
	{
        if (player.GetComponent<ClientController>().playerRole == ClientController.Role.impostor)
        {
            impostorsUI.SetActive(true);
        }
        else
        {
            crewUI.SetActive(true);
        }
    }
    public void GameStart()
	{
        int numPlayers = playersList.Count;

		while (impostorsCount >= numPlayers - impostorsCount)
		{
            impostorsCount -= 1;
		}

        List<GameObject> playersCopy = new List<GameObject>(playersList);

        // Assign Impostors
        for (int i = 0; i < impostorsCount; i++)
		{
            int n = Random.Range(0, playersCopy.Count);
            GameObject newImpostor = playersCopy[n];
            playersCopy.RemoveAt(n);

            newImpostor.GetComponent<ClientController>().playerRole = ClientController.Role.impostor;
        }

		// Give Crew to the rest
		foreach (GameObject crew in playersList)
		{
            crew.GetComponent<ClientController>().playerRole = ClientController.Role.crew;
		}

        gameState = GameState.onGame;
    }
}