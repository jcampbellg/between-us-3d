using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivertedPower : MonoBehaviour
{
    public GameObject acceptPower;
	
	private void OnEnable()
	{
		GameObject localPlayer = GameObject.FindGameObjectWithTag("GameState").GetComponent<PlayerSettings>().localPlayer;
		if (localPlayer.GetComponent<ClientController>().playerRole == ClientController.Role.impostor)
		{
			int n = Random.Range(0, acceptPower.transform.childCount);
			acceptPower.transform.GetChild(n).gameObject.SetActive(true);
		}
	}
}
