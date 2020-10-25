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
		openUIPanel
	};

    public string label;
    public string instructions;
	public Tasks type;
}