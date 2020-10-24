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

                if (layer == "Task")
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
        GameObject playerHit = hit.transform.gameObject;
        float killDistance = settings.clickPlayerDistance * settings.killDistance;

        if ( hit.distance < killDistance && this.GetComponent<ClientController>().playerRole == ClientController.Role.lobby)
		{
            string playerName = playerHit.GetComponent<ClientController>().playerName;
            info.text = playerName + "\n" + "On Kill Distance";
        }
        else if (hit.distance < killDistance && this.GetComponent<ClientController>().playerRole == ClientController.Role.impostor && playerHit.GetComponent<ClientController>().playerRole != ClientController.Role.impostor)
		{
            string playerName = playerHit.GetComponent<ClientController>().playerName;
            info.text = playerName + "\n" + "[Q] Kill";
        }
        else if (this.GetComponent<ClientController>().playerRole == ClientController.Role.impostor && playerHit.GetComponent<ClientController>().playerRole == ClientController.Role.impostor)
		{
            string playerName = playerHit.GetComponent<ClientController>().playerName;
            info.text = playerName + "\n" + "He is Impostor";
        }
        else
        {
            string playerName = playerHit.GetComponent<ClientController>().playerName;
            info.text = playerName;
        }
    }
}