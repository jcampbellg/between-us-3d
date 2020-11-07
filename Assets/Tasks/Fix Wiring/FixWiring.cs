using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixWiring : MonoBehaviour
{
    public TasksState tasksState;
	public int onFixWire = 0;

	private void OnEnable()
	{
		for (int i = 0; i < tasksState.wireTaskToActive.Count; i++)
		{
			tasksState.wireTaskList[tasksState.wireTaskToActive[i]].GetComponent<TaskController>().task.step = "(" + (i + 1) + ")";
		}

		GameObject localPlayer = GameObject.FindGameObjectWithTag("GameState").GetComponent<PlayerSettings>().localPlayer;

		if (localPlayer.GetComponent<ClientController>().playerRole == ClientController.Role.impostor)
		{
			foreach (int item in tasksState.wireTaskToActive)
			{
				tasksState.wireTaskList[item].SetActive(true);
			}
		}
		else
		{
			tasksState.wireTaskList[tasksState.wireTaskToActive[0]].SetActive(true);
		}
	}

	public void NextFixWire()
	{
		onFixWire += 1;
		if (onFixWire < tasksState.wireTaskToActive.Count)
		{
			tasksState.wireTaskList[tasksState.wireTaskToActive[onFixWire]].SetActive(true);
		}
	}
}
