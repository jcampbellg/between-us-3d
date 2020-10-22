using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
	public Slider fogDensity;
	public Slider playerSpeed;
	public Slider killDistance;

	private void OnEnable()
	{
		Settings settings = this.GetComponent<TaskUI>().settings;
		fogDensity.value = settings.fogDensity;
		playerSpeed.value = settings.playerSpeed;
		killDistance.value = settings.killDistance;
	}
	public void ChangeFogDistance(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		if (client)
		{
			client.GetComponent<ClientController>().ChangeFloatSetting(Settings.Setting.fogDensity, value);
		}
	}
	public void ChangePlayerSpeed(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		if (client)
		{
			client.GetComponent<ClientController>().ChangeFloatSetting(Settings.Setting.playerSpeed, value);
		}
	}
	public void ChangeKillDistance(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;
		
		if (client)
		{
			client.GetComponent<ClientController>().ChangeFloatSetting(Settings.Setting.killDistance, value);
		}
	}
	public void ResetSettings()
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		if (client)
		{
			client.GetComponent<ClientController>().ResetSettings();
		}
	}
}
