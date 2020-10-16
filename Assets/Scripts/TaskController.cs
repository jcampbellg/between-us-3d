using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    public Task task;
    public void ActionTask(GameObject client)
	{
		switch (task.id)
		{
			case Task.Tasks.changeSkin:
				client.GetComponent<Client>().ChangeSkin();
				break;
			case Task.Tasks.changeSettings:
				break;
			default:
				break;
		}
	}
}