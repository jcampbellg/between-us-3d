using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUI : MonoBehaviour
{
	public Settings settings;
	public GameObject client;
	void Update()
	{
		if (settings.isTaskOpen)
		{
			if (Input.GetButtonDown("Cancel"))
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
}
