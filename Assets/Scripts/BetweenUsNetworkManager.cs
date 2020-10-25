using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class BetweenUsNetworkManager : NetworkManager
{
	public Settings settings;
	public List<Skin> skins;
	public TMP_InputField ipInput;
	public TMP_InputField playerNameInput;
	public string playerName = "";

	public override void OnServerAddPlayer(NetworkConnection conn)
	{
		GameObject newPlayer = Instantiate(playerPrefab);
		newPlayer.transform.position = Vector3.zero;
		SetPlayerName(newPlayer);
		SelectRandomSkin(newPlayer);
		NetworkServer.AddPlayerForConnection(conn, newPlayer);
	}
	void SetPlayerName(GameObject player)
	{
		player.GetComponent<ClientController>().playerName = "Loading";
	}
	void SelectRandomSkin(GameObject player)
	{
		int n = Random.Range(0, skins.Count);
		Skin skin = skins[n];
		player.GetComponent<SkinRenderer>().skin = skin;
		skins.RemoveAt(n);
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
}