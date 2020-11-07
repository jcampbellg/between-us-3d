using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUI : MonoBehaviour
{
	public PlayerSettings playerSettings;
	public GameObject client;
	public TaskController taskObject;
	public bool canClose = true;

	void Update()
	{
		if (playerSettings.isTaskOpen && canClose)
		{
			if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Action"))
			{
				gameObject.SetActive(false);
			}
		}
	}
	private void OnEnable()
	{
		playerSettings.isMenuOpen = true;
		playerSettings.isTaskOpen = true;
		Cursor.lockState = CursorLockMode.None;
	}
	private void OnDisable()
	{
		playerSettings.isMenuOpen = false;
		playerSettings.isTaskOpen = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	public void FinishUI()
	{
		gameObject.SetActive(false);
		taskObject.HideTask();
		client.GetComponent<ClientController>().AddToFinishTasks();
	}
}
