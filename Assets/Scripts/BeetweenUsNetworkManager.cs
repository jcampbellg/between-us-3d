using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BeetweenUsNetworkManager : NetworkManager
{
	public override void OnServerAddPlayer(NetworkConnection conn)
	{
		GameObject newPlayer = Instantiate(playerPrefab);
		newPlayer.transform.position = Vector3.zero;

		NetworkServer.AddPlayerForConnection(conn, newPlayer);
	}
}