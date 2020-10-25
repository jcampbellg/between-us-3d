using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUI : MonoBehaviour
{
	public Settings settings;
	public GameObject client;
	public TaskController taskObject;
	void Update()
	{
		if (settings.isTaskOpen)
		{
			if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Action"))
			{
				gameObject.SetActive(false);
			}
		}
	}
	private void OnEnable()
	{
		settings.isMenuOpen = true;
		settings.isTaskOpen = true;
		Cursor.lockState = CursorLockMode.None;
	}
	private void OnDisable()
	{
		settings.isMenuOpen = false;
		settings.isTaskOpen = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	public void FinishUI()
	{
		gameObject.SetActive(false);
		taskObject.HideTask();
		client.GetComponent<ClientController>().AddToFinishTasks();
	}
}
