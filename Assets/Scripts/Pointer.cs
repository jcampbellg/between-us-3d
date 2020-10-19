using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pointer : MonoBehaviour
{
    public float clickActionDistance = 2.5f;
    public LayerMask clickableObjects;
    public TextMeshProUGUI info;
    public Settings settings;
    public GameObject pointerCanvas;
    public bool canUse = false;
    private void Update()
	{
        if (canUse)
		{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float clickPlayerDistance = settings.clickPlayerDistance;
            if (!settings.isMenuOpen && Physics.Raycast(ray, out RaycastHit hit, clickPlayerDistance, clickableObjects))
            {
                string layer = LayerMask.LayerToName(hit.transform.gameObject.layer);

                if (layer == "Clickable")
				{
                    if (hit.distance < clickActionDistance)
                        OnTask(hit);
                    else
                        info.text = "";
                } 
                else if (layer == "Player")
				{
                    OnPlayer(hit);
                }
            }
            else
            {
                info.text = "";
            }

            if (!settings.isMenuOpen && !pointerCanvas.activeSelf)
			{
                pointerCanvas.SetActive(true);
            }
            else if (settings.isMenuOpen && pointerCanvas.activeSelf)
			{
                pointerCanvas.SetActive(false);
            }
        }
    }
    void OnTask(RaycastHit hit)
	{
        GameObject taskObject = hit.transform.gameObject;

        TaskController taskCtl = taskObject.GetComponent<TaskController>();
        info.text = taskCtl.task.label + "\n" + taskCtl.task.instructions;

        if (Input.GetButtonDown("Action"))
        {
            taskCtl.ActionTask(this.gameObject);
        }
    }
    void OnPlayer(RaycastHit hit)
    {
        GameObject player = hit.transform.gameObject;

        string playerName = player.GetComponent<ClientController>().playerName;
        info.text = playerName;
    }
}