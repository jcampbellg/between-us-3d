using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManager : MonoBehaviour
{
    public Door[] cafeteria;

    public void OpenCafeteria(bool isOpen)
	{
		foreach (Door door in cafeteria)
		{
			door.isOpen = isOpen;
		}
	}
}
