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
	private void Start()
	{
		if (hideOnLobby)
			this.gameObject.SetActive(false);
		else
			mapUI.SetActive(true);
	}
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
					panelToOpen.GetComponent<TaskUI>().client = client;
					panelToOpen.SetActive(true);
				}
				break;
			default:
				break;
		}
	}
	private void OnEnable()
	{
		mapUI.SetActive(true);
	}
	private void OnDisable()
	{
		if (mapUI)
			mapUI.SetActive(false);
	}
}