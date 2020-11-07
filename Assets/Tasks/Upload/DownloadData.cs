using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadData : MonoBehaviour
{
	public GameObject uploadData;
	private void OnEnable()
	{
		int n = Random.Range(0, transform.childCount);
		this.transform.GetChild(n).gameObject.SetActive(true);

		GameObject localPlayer = GameObject.FindGameObjectWithTag("GameState").GetComponent<PlayerSettings>().localPlayer;
		if (localPlayer.GetComponent<ClientController>().playerRole == ClientController.Role.impostor)
		{
			uploadData.SetActive(true);
		}
	}
}
