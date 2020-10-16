using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
	public Settings settings;
	public Slider slider;
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
		slider.value = settings.fogDistance;
	}
	public void ChangeFogDistance()
	{
		settings.fogDistance = slider.value;
		if (client)
		{
			client.GetComponent<Client>().ChangeFog(settings.fogDistance);
		}
	}
}
