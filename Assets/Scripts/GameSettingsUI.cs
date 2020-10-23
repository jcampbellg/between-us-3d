using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSettingsUI : MonoBehaviour
{
	public Slider fogDensity;
	public Slider playerSpeed;
	public Slider killDistance;
	public Slider impostorsCount;
	public TextMeshProUGUI impostorsCountText;

	private void OnEnable()
	{
		Settings settings = this.GetComponent<TaskUI>().settings;
		fogDensity.value = -(settings.fogDensity / 0.4f) + 1.0f;
		playerSpeed.value = settings.playerSpeed;
		killDistance.value = settings.killDistance;
		impostorsCount.value = settings.impostorsCount;
		impostorsCountText.text = settings.impostorsCount + " Impostors";
	}
	public void ChangeFogDistance(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		float fogValue = (1.0f - value) * 0.4f;

		if (client)
		{
			client.GetComponent<ClientController>().ChangeFloatSetting(Settings.Setting.fogDensity, fogValue);
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
	public void ChangeImpostorsCounts(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		impostorsCountText.text = value + " Impostors";

		if (client)
		{
			client.GetComponent<ClientController>().ChangeIntSetting(Settings.Setting.impostorsCount, (int) value);
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
