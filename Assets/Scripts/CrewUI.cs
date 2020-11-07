using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrewUI : MonoBehaviour
{
	public TasksState tastState;
	public TextMeshProUGUI taskListUI;
	bool showTask = true;

	private void Update()
	{
		if (Input.GetButtonDown("Task"))
		{
			showTask = !showTask;
		}

		taskListUI.text = "";

		if (showTask)
		{
			foreach (Task task in tastState.localTaskList)
			{
				taskListUI.text += task.label + " " + task.step + "\n";
			}
		}
	}
}
