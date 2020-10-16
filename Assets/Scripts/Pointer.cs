using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pointer : MonoBehaviour
{
    public float clickDistance = 2.0f;
    public LayerMask clickableObjects;
    public TextMeshProUGUI info;
    public Settings settings;
    public bool canUse = false;
    private void Update()
	{
        if (canUse)
		{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!settings.isMenuOpen && Physics.Raycast(ray, out RaycastHit hit, clickDistance, clickableObjects))
            {
                GameObject taskObject = hit.transform.gameObject;

                TaskController taskCtl = taskObject.GetComponent<TaskController>();
                info.text = taskCtl.task.label + "\n" + taskCtl.task.instructions;

                if (Input.GetButtonDown("Action"))
                {
                    taskCtl.ActionTask(this.gameObject);
                }
            }
            else
            {
                info.text = "";
            }
        }
    }
}