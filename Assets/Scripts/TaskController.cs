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
			case Task.Tasks.fixWires:
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