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
	public Slider commonTaskCountSlider;
	public TextMeshProUGUI commonTaskCountText;
	public Slider shortTaskCountSlider;
	public TextMeshProUGUI shortTaskCountText;
	public Slider longTaskCountSlider;
	public TextMeshProUGUI longTaskCountText;
	public Slider killDistance;
	public Slider impostorCount;
	public TextMeshProUGUI impostorCountText;
	
	private void OnEnable()
	{
		fogDensity.value = -(gameSettings.fogDensity / 0.4f) + 1.0f;
		playerSpeed.value = gameSettings.playerSpeed;

		commonTaskCountSlider.value = gameSettings.commonTaskCount;
		commonTaskCountText.text= gameSettings.commonTaskCount + " Common Tasks";
		shortTaskCountSlider.value = gameSettings.shortTaskCount;
		shortTaskCountText.text= gameSettings.shortTaskCount + " Short Tasks";
		longTaskCountSlider.value = gameSettings.longTaskCount;
		longTaskCountText.text = gameSettings.longTaskCount + " Long Tasks";

		killDistance.value = gameSettings.killDistance;

		impostorCount.value = gameSettings.impostorCount;
		impostorCountText.text = gameSettings.impostorCount + " Impostors";
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
	public void ChangeImpostorCount(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		impostorCountText.text = value + " Impostors";

		if (client)
		{
			client.GetComponent<ClientController>().ChangeIntSetting(GameSettings.Setting.impostorCount, (int) value);
		}
	}

	public void ChangeCommonTaskCount(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		commonTaskCountText.text = value + " Common Tasks";

		if (client)
		{
			client.GetComponent<ClientController>().ChangeIntSetting(GameSettings.Setting.commonTaskCount, (int)value);
		}
	}
	public void ChangeShortTaskCount(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		shortTaskCountText.text = value + " Short Tasks";

		if (client)
		{
			client.GetComponent<ClientController>().ChangeIntSetting(GameSettings.Setting.shortTaskCount, (int)value);
		}
	}
	public void ChangeLongTaskCount(float value)
	{
		GameObject client = this.GetComponent<TaskUI>().client;

		longTaskCountText.text = value + " Long Tasks";

		if (client)
		{
			client.GetComponent<ClientController>().ChangeIntSetting(GameSettings.Setting.longTaskCount, (int)value);
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
