using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
	public Settings settings;
    public Task task;
	public GameObject panelToOpen;
    public void ActionTask(GameObject client)
	{
		switch (task.id)
		{
			case Task.Tasks.changeSkin:
				client.GetComponent<ClientController>().ActionTask(task.id);
				break;
			case Task.Tasks.changeSettings:
				if (panelToOpen)
				{
					settings.OpenTask();
					panelToOpen.SetActive(true);
					panelToOpen.GetComponent<GameSettingsUI>().client = client;
				}
				break;
			default:
				break;
		}
	}
}