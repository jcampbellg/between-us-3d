using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class BeetweenUsNetworkManager : NetworkManager
{
	public Settings settings;
	public List<Skin> skins;
	public TMP_InputField ipInput;
	public TMP_InputField playerNameInput;
	public GameObject clickableCanvas;
	public string playerName = "";

	public override void Start()
	{
		settings.Restart();
		base.Start();
	}
	public override void OnServerAddPlayer(NetworkConnection conn)
	{
		GameObject newPlayer = Instantiate(playerPrefab);
		newPlayer.transform.position = Vector3.zero;
		SelectRandomSkin(newPlayer);
		SetPlayerName(newPlayer);
		NetworkServer.AddPlayerForConnection(conn, newPlayer);
	}
	void SetPlayerName(GameObject player)
	{
		if (playerName == "")
		{
			playerName = "Player " + numPlayers+1;
		}
		player.GetComponent<ClientController>().playerName = playerName;
	}
	void SelectRandomSkin(GameObject player)
	{
		int n = Random.Range(0, skins.Count);
		Skin skin = skins[n];
		player.GetComponent<SkinRenderer>().skin = skin;
		skins.RemoveAt(n);
	}
	public override void OnClientConnect(NetworkConnection conn)
	{
		clickableCanvas.SetActive(true);
		
		base.OnClientConnect(conn);
	}

	public void OnEndEditIp()
	{
		networkAddress = ipInput.text;
	}
	public void OnEndEditPlayerName()
	{
		playerName = playerNameInput.text;
	}
	public void SelectFirstSkin(GameObject player)
	{
		int n = 0;
		Skin newSkin = skins[n];
		Skin oldSkin = player.GetComponent<SkinRenderer>().skin;
		player.GetComponent<SkinRenderer>().skin = newSkin;
		skins.RemoveAt(n);
		skins.Add(oldSkin);
	}
	public override void OnStartServer()
	{
		base.OnStartServer();
	}
	public override void OnServerDisconnect(NetworkConnection conn)
	{
		NetworkIdentity identity = conn.identity;
		uint netId = identity.netId;

		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			if (player.GetComponent<NetworkIdentity>().netId == netId)
			{
				// Player Who Left The Game
				skins.Add(player.GetComponent<SkinRenderer>().skin);
			}
		}

		NetworkServer.RemovePlayerForConnection(conn, true);
	}
}