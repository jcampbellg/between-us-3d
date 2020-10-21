using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Task")]
public class Task : ScriptableObject
{
    public enum Tasks
	{
		changeSkin,
		changeSettings,
		fixWires
	};

    public string label;
    public string instructions;
	public Tasks id;
}