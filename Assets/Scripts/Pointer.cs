using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pointer : MonoBehaviour
{
    public float clickActionDistance = 2.5f;
    public LayerMask clickableObjects;
    public TextMeshProUGUI info;
    public GameObject interactLayout;
    public TextMeshProUGUI instructions;
    public TextMeshProUGUI key;
    PlayerSettings playerSettings;
    GameSettings gameSettings;
    GameState gameState;
    public GameObject pointerCanvas;
    public bool canUse = false;

	private void Start()
	{
        GameObject gameStateObject = GameObject.FindGameObjectWithTag("GameState");
        playerSettings = gameStateObject.GetComponent<PlayerSettings>();
        gameSettings = gameStateObject.GetComponent<GameSettings>();
        gameState = gameStateObject.GetComponent<GameState>();
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
					{
                        interactLayout.SetActive(false);
                        info.text = "";
                    }
                } 
                else if (layer == "Player")
				{
                    OnPlayer(hit);
                }
                else if (layer == "DeadBody")
				{
                    OnDeadBody(hit);
				}
            }
            else
            {
                interactLayout.SetActive(false);
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
                info.text = taskCtl.task.label;
                key.text = "E";
                instructions.text = taskCtl.task.instructions;
                interactLayout.SetActive(true);
                if (Input.GetButtonDown("Action"))
                {
                    taskCtl.ActionTask(this.gameObject);
                }
                break;
			case ClientController.Role.impostor:
                info.text = taskCtl.task.label + "\n" + "Don't move to pretend";
                interactLayout.SetActive(false);
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
        bool isGhost = this.GetComponent<ClientController>().isGhost;
        ClientController.Role playerHitRole = playerHit.GetComponent<ClientController>().playerRole;
        string playerHitName = playerHit.GetComponent<ClientController>().playerName;

        if (isGhost)
        {
            info.text = playerHitName;
            interactLayout.SetActive(false);
        }
        else
        {
            if (gameState.gameState == GameState.State.onMeeting)
			{
                info.text = playerHitName;
                key.text = "M1";
                instructions.text = "Vote";
                interactLayout.SetActive(true);
            }
            else
			{
                switch (playerRole)
                {
                    case ClientController.Role.lobby:
                        if (hit.distance < killDistance)
                        {
                            info.text = playerHitName + "\n" + "On Kill Distance";
                            interactLayout.SetActive(false);
                        }
                        else
                        {
                            info.text = playerHitName;
                            interactLayout.SetActive(false);
                        }
                        break;
                    case ClientController.Role.crew:
                        info.text = playerHitName;
                        break;
                    case ClientController.Role.impostor:
                        if (playerHitRole == ClientController.Role.impostor)
                        {
                            info.text = playerHitName + "\n" + "He is Impostor";
                            interactLayout.SetActive(false);
                        }
                        else
                        {
                            if (hit.distance < killDistance)
                            {
                                info.text = playerHitName;
                                key.text = "M1";
                                instructions.text = "Kill";
                                interactLayout.SetActive(true);

                                if (Input.GetButtonDown("Kill"))
                                    this.GetComponent<ClientController>().KillCrew(playerHit);
                            }
                            else
                                info.text = playerHitName;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
    void OnDeadBody(RaycastHit hit)
	{
        GameObject deadBodyHit = hit.transform.gameObject;
        string playerHitName = deadBodyHit.GetComponent<DeadBody>().playerName;
        bool isGhost = this.GetComponent<ClientController>().isGhost;

        if (isGhost)
		{
            info.text = playerHitName + "'s Body";
        }
        else
		{
            info.text = playerHitName + "'s Body";
            key.text = "M1";
            instructions.text = "Report";
            interactLayout.SetActive(true);

            if (Input.GetButtonDown("Report"))
                this.GetComponent<ClientController>().CmdReport();
        }
    }
}