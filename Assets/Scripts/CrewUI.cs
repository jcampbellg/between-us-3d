using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrewUI : MonoBehaviour
{
	public List<GameObject> allTasks;
	public List<GameObject> tasksList;
	public GameSettings gameSettings;
	public TextMeshProUGUI taskListUI;
	bool showTask = true;

	public void AssignTasks()
	{
		int count = gameSettings.tasksCount;

		List<GameObject> tasksAvalaible = new List<GameObject>(allTasks);
		tasksList = new List<GameObject>();

		for (int i = 0; i < count; i++)
		{
			int n = Random.Range(0, tasksAvalaible.Count);
			GameObject taskObject = tasksAvalaible[n];

			tasksAvalaible.RemoveAt(n);
			tasksList.Add(taskObject);
		}

		foreach (GameObject taskObject in tasksAvalaible)
		{
			taskObject.GetComponent<TaskController>().HideTask();
		}
	}
	private void Update()
	{
		if (Input.GetButtonDown("Task"))
		{
			showTask = !showTask;
		}

		taskListUI.text = "";

		if (showTask)
		{
			foreach (GameObject task in tasksList)
			{
				taskListUI.text += task.GetComponent<TaskController>().task.label + "\n";
			}
		}

		// Remove Done Tasks
		foreach (GameObject task in tasksList)
		{
			if (task.layer == LayerMask.NameToLayer("Task Finish"))
			{
				tasksList.Remove(task);
			}
		}
	}
}
