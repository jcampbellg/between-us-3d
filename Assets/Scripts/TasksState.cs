using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

[System.Serializable]
public class SyncListInt : SyncList<int> { };

public class TasksState : NetworkBehaviour
{
    GameSettings gameSettings;
    public Slider taskbar;
    public float animSpeed = 0.5f;
    public GameObject taskbarPanel;
    [SyncVar]
    public int totalTasksDone = 0;
    [SyncVar(hook = nameof(OnTotalTask))]
    public int totalTasks = 0;
    public List<Task> localTaskList;
    public List<GameObject> wireTaskList;
    public List<GameObject> commonTaskList;
    public List<GameObject> shortTaskList;

    public readonly SyncListInt commonTaskToActive = new SyncListInt();
    public readonly SyncListInt wireTaskToActive = new SyncListInt();

    private void Start()
	{
        gameSettings = this.GetComponent<GameSettings>();

        if (isServer)
		{
            for (int i = 0; i < wireTaskList.Count; i++)
            {
                wireTaskToActive.Add(i);
            }

            while (wireTaskToActive.Count > 3)
            {
                int n = Random.Range(0, wireTaskToActive.Count);
                wireTaskToActive.Remove(n);
            }
		}
    }

    public void AssignCommonTasks()
	{
        // Server
        switch (gameSettings.commonTaskCount)
		{
            case 0:
                break;
            case 1:
                int n = Random.Range(0, commonTaskList.Count);
                commonTaskToActive.Add(n);
                break;
			default:
				for (int i = 0; i < commonTaskList.Count; i++)
				{
                    commonTaskToActive.Add(i);
                }
				break;
		}
	}

    public void ActiveCommonTasks()
	{
		foreach (int task in commonTaskToActive)
		{
            commonTaskList[task].SetActive(true);
        }
	}

    public void AssignShortTasks()
	{
        // Client
        List<GameObject> shortTaskListToActive = new List<GameObject>(shortTaskList);

        while (shortTaskListToActive.Count > gameSettings.shortTaskCount)
        {
            int n = Random.Range(0, shortTaskListToActive.Count);
            shortTaskListToActive.RemoveAt(n);
        }

		foreach (GameObject shortTask in shortTaskListToActive)
		{
            shortTask.SetActive(true);
		}
    }

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
