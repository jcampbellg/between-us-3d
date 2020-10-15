using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BeetweenUsNetworkManager : NetworkManager
{
	public List<Skin> skins;
	public override void OnServerAddPlayer(NetworkConnection conn)
	{
		GameObject newPlayer = Instantiate(playerPrefab);
		newPlayer.transform.position = Vector3.zero;
		SelectRandomSkin(newPlayer);
		NetworkServer.AddPlayerForConnection(conn, newPlayer);
	}
	void SelectRandomSkin(GameObject player)
	{
		int n = Random.Range(0, skins.Count);
		Skin skin = skins[n];
		player.GetComponent<SkinRenderer>().skin = skin;
		skins.RemoveAt(n);
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
				skins.Add(player.GetComponent<SkinRenderer>().skin);
			}
		}

		NetworkServer.RemovePlayerForConnection(conn, true);
	}
}