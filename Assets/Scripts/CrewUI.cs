using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewUI : MonoBehaviour
{
	public List<GameObject> allTasks;
	public List<GameObject> tasksList;
	public Settings settings;

	public void AssignTasks()
	{
		int count = settings.tasksCount;

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
}
