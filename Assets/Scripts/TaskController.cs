using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    public Task task;
	public GameObject panelToOpen;

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
		this.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		GameObject gameState = GameObject.FindGameObjectWithTag("GameState");
		if (gameState)
		{
			GameObject localPlayer = GameObject.FindGameObjectWithTag("GameState").GetComponent<PlayerSettings>().localPlayer;
			if (localPlayer)
			{
				if (localPlayer.GetComponent<ClientController>().playerRole == ClientController.Role.crew)
				{
					localPlayer.GetComponent<ClientController>().AddToTotalTaskCount(task.stepsAdd);
					if (task.type == Task.Tasks.openUIPanel)
						gameState.GetComponent<TasksState>().localTaskList.Add(task);
				}
			}
		}
	}
	private void OnDisable()
	{
		GameObject gameState = GameObject.FindGameObjectWithTag("GameState");
		if (gameState)
		{
			GameObject localPlayer = GameObject.FindGameObjectWithTag("GameState").GetComponent<PlayerSettings>().localPlayer;
			if (localPlayer)
			{
				if (localPlayer.GetComponent<ClientController>().playerRole == ClientController.Role.crew)
				{
					if (task.type == Task.Tasks.openUIPanel)
						gameState.GetComponent<TasksState>().localTaskList.Remove(task);
				}
			}
		}
	}
}