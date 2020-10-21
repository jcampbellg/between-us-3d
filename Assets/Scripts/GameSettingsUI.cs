using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
	public Slider fogDistance;
	public Slider playerSpeed;

	private void OnEnable()
	{
		Settings settings = this.GetComponent<TaskUI>().settings;
		fogDistance.value = settings.fogDensity;
		playerSpeed.value = settings.playerSpeed;
	}
	public void ChangeFogDistance()
	{
		Settings settings = this.GetComponent<TaskUI>().settings;
		GameObject client = this.GetComponent<TaskUI>().client;

		settings.fogDensity = fogDistance.value;
		if (client)
		{
			client.GetComponent<ClientController>().ChangeSetting(Settings.Setting.fogDensity);
		}
	}
	public void ChangePlayerSpeed()
	{
		Settings settings = this.GetComponent<TaskUI>().settings;
		GameObject client = this.GetComponent<TaskUI>().client;

		settings.playerSpeed = playerSpeed.value;
		if (client)
		{
			client.GetComponent<ClientController>().ChangeSetting(Settings.Setting.playerSpeed);
		}
	}
}
