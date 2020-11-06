using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSettingsUI : MonoBehaviour
{
	public GameSettings gameSettings;
	public Slider fogDensity;
	public Slider playerSpeed;
	public Slider tasksCount;
	public TextMeshProUGUI tasksCountText;
	public Slider killDistance;
	public Slider impostorsCount;
	public TextMeshProUGUI impostorsCountText;
	
	private void OnEnable()
	{
		fogDensity.value = -(gameSettings.fogDensity / 0.4f) + 1.0f;
		playerSpeed.value = gameSettings.playerSpeed;
		tasksCount.value = gameSettings.tasksCount;
		killDistance.value = gameSettings.killDistance;
		impostorsCount.value = gameSettings.impostorsCount;
		impostorsCountText.text = gameSettings.impostorsCount + " Impostors";
	}
	public void ChangeFogDistance(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		float fogValue = (1.0f - value) * 0.4f;

		if (client)
		{
			client.GetComponent<ClientController>().ChangeFloatSetting(GameSettings.Setting.fogDensity, fogValue);
		}
	}
	public void ChangePlayerSpeed(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		if (client)
		{
			client.GetComponent<ClientController>().ChangeFloatSetting(GameSettings.Setting.playerSpeed, value);
		}
	}
	public void ChangeKillDistance(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;
		
		if (client)
		{
			client.GetComponent<ClientController>().ChangeFloatSetting(GameSettings.Setting.killDistance, value);
		}
	}
	public void ChangeImpostorsCounts(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		impostorsCountText.text = value + " Impostors";

		if (client)
		{
			client.GetComponent<ClientController>().ChangeIntSetting(GameSettings.Setting.impostorsCount, (int) value);
		}
	}
	public void ChangeTasksCounts(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		tasksCountText.text = value + " Tasks";

		if (client)
		{
			client.GetComponent<ClientController>().ChangeIntSetting(GameSettings.Setting.tasksCount, (int)value);
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
