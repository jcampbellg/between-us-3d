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
				settings.CloseTask();
				gameObject.SetActive(false);
			}
		}
	}
}
