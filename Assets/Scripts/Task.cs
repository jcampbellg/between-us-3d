using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Task")]
public class Task : ScriptableObject
{
    public enum Tasks
	{
		changeSkin,
		openGameSettings,
		openUIPanel,
		parentTask
	};

    public string label;
    public string instructions;
	public Tasks type;
	public string step;
	public int stepsAdd;
	public int stepsSubtract;
}