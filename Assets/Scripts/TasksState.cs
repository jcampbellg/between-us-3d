using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class TasksState : NetworkBehaviour
{
    public Slider taskbar;
    public float animSpeed = 0.5f;
    public GameObject taskbarPanel;
    [SyncVar]
    public int totalTasksDone = 0;
    [SyncVar(hook = nameof(OnTotalTask))]
    public int totalTasks = 0;

    public void OnTotalTask(int oldInt, int newInt)
	{
        taskbar.maxValue = newInt;
	}

	private void Update()
	{
        if (Mathf.Abs(totalTasksDone - taskbar.value) > 0.1f)
        {
            taskbar.value = Mathf.Lerp(taskbar.value, totalTasksDone, animSpeed * Time.deltaTime);
        }
        else
            taskbar.value = totalTasksDone;
    }
}
