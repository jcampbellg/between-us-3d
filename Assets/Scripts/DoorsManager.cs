using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManager : MonoBehaviour
{
    public GameObject[] lobby;

    public void OpenLobby(bool isOpen)
	{
		foreach (GameObject door in lobby)
		{
			door.GetComponent<Door>().isOpen = true;
		}
	}
}
