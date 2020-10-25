using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    public Task task;
	public GameObject panelToOpen;
	public GameObject mapUI;
	public bool hideOnLobby = true;

	public void ActionTask(GameObject client)
	{
		switch (task.type)
		{
			case Task.Tasks.changeSkin:
				client.GetComponent<ClientController>().ActionTask(task.type);
				break;
			case Task.Tasks.openGameSettings:
			case Task.Tasks.openUIPanel:
				if (panelToOpen)
				{
					TaskUI taskUI = panelToOpen.GetComponent<TaskUI>();
					taskUI.client = client;
					taskUI.taskObject = this;
					panelToOpen.SetActive(true);
				}
				break;
			default:
				break;
		}
	}
	public void HideTask()
	{
		if (mapUI)
			mapUI.SetActive(false);
		this.gameObject.layer = LayerMask.NameToLayer("Task Finish");
	}
}