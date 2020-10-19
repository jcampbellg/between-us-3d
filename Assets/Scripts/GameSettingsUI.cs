using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
	public Settings settings;
	public Slider fogDistance;
	public Slider playerSpeed;
	public GameObject client;
	private void Update()
	{
		if (settings.isTaskOpen)
		{
			if (Input.GetButtonDown("Cancel"))
			{
				settings.CloseTask();
				gameObject.SetActive(false);
			}
		}
	}
	private void OnEnable()
	{
		fogDistance.value = settings.fogDensity;
		playerSpeed.value = settings.playerSpeed;
	}
	public void ChangeFogDistance()
	{
		settings.fogDensity = fogDistance.value;
		if (client)
		{
			client.GetComponent<ClientController>().ChangeSetting(Settings.Setting.fogDensity);
		}
	}
	public void ChangePlayerSpeed()
	{
		settings.playerSpeed = playerSpeed.value;
		if (client)
		{
			client.GetComponent<ClientController>().ChangeSetting(Settings.Setting.playerSpeed);
		}
	}
}
