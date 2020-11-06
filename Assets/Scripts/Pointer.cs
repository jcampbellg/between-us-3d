using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pointer : MonoBehaviour
{
    public float clickActionDistance = 2.5f;
    public LayerMask clickableObjects;
    public TextMeshProUGUI info;
    PlayerSettings playerSettings;
    GameSettings gameSettings;
    public GameObject pointerCanvas;
    public bool canUse = false;
	private void Start()
	{
        GameObject gameStateObject = GameObject.FindGameObjectWithTag("GameState");
        playerSettings = gameStateObject.GetComponent<PlayerSettings>();
        gameSettings = gameStateObject.GetComponent<GameSettings>();
    }
	private void Update()
	{
        if (canUse)
		{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float clickPlayerDistance = gameSettings.clickPlayerDistance;

            if (!playerSettings.isMenuOpen && Physics.Raycast(ray, out RaycastHit hit, clickPlayerDistance, clickableObjects))
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

            if (!playerSettings.isMenuOpen && !pointerCanvas.activeSelf)
            {
                pointerCanvas.SetActive(true);
            }
            else if (playerSettings.isMenuOpen && pointerCanvas.activeSelf)
            {
                pointerCanvas.SetActive(false);
            }
        }
    }
    void OnTask(RaycastHit hit)
	{
        GameObject taskObject = hit.transform.gameObject;
        TaskController taskCtl = taskObject.GetComponent<TaskController>();

        ClientController.Role playerRole = this.GetComponent<ClientController>().playerRole;

		switch (playerRole)
		{
			case ClientController.Role.lobby:
			case ClientController.Role.crew:
                info.text = taskCtl.task.label + "\n" + taskCtl.task.instructions;
                if (Input.GetButtonDown("Action"))
                {
                    taskCtl.ActionTask(this.gameObject);
                }
                break;
			case ClientController.Role.impostor:
                info.text = taskCtl.task.label + "\n" + "Don't move to pretend";
                break;
			default:
				break;
		}
    }
    void OnPlayer(RaycastHit hit)
    {
        GameObject playerHit = hit.transform.gameObject;
        float killDistance = gameSettings.clickPlayerDistance * gameSettings.killDistance;

        ClientController.Role playerRole = this.GetComponent<ClientController>().playerRole;
        ClientController.Role playerHitRole = playerHit.GetComponent<ClientController>().playerRole;
        string playerHitName = playerHit.GetComponent<ClientController>().playerName;

        switch (playerRole)
        {
            case ClientController.Role.lobby:
                if (hit.distance < killDistance)
                    info.text = info.text = playerHitName + "\n" + "On Kill Distance";
                else
                    info.text = info.text = playerHitName;
                break;
            case ClientController.Role.crew:
                info.text = info.text = playerHitName;
                break;
            case ClientController.Role.impostor:
                if (playerHitRole == ClientController.Role.impostor)
                {
                    info.text = playerHitName + "\n" + "He is Impostor";
                }
                else
                {
                    if (hit.distance < killDistance)
                        info.text = info.text = playerHitName + "\n" + "[Q] Kill";
                    else
                        info.text = info.text = playerHitName;
                }
                break;
            default:
                break;
        }
    }
}