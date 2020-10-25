using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class TasksState : NetworkBehaviour
{
    public Slider taskbar;
    [SyncVar(hook = nameof(OnTaskDone))]
    public int totalTasksDone = 0;
    [SyncVar(hook = nameof(OnTotalTask))]
    public int totalTasks = 0;

    public void OnTotalTask(int oldInt, int newInt)
	{
        taskbar.maxValue = newInt;
	}

    public void OnTaskDone(int oldInt, int newInt)
    {
        taskbar.value = newInt;
    }
}
