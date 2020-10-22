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
		switch (task.type)
		{
			case Task.Tasks.changeSkin:
				client.GetComponent<ClientController>().ActionTask(task.type);
				break;
			case Task.Tasks.openUIPanel:
				if (panelToOpen)
				{
					settings.OpenTask();
					panelToOpen.GetComponent<TaskUI>().client = client;
					panelToOpen.SetActive(true);
				}
				break;
			default:
				break;
		}
	}
}