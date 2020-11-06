using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClientController : NetworkBehaviour
{
    public enum Role
	{
        lobby,
        crew,
        impostor
	};
    Transform meetingPosition;
    PlayerSettings playerSettings;
    GameSettings gameSettings;
    GameState gameState;
    public GameObject canvas;
    public GameObject mapCanvas;
    [SyncVar]
    public string playerName = "";
    [SyncVar(hook = nameof(OnGameStart))]
    public Role playerRole = Role.lobby;
    [SyncVar]
    public bool isReady = false;

    void Start()
    {
        meetingPosition = transform;
        GameObject gameStateObject = GameObject.FindGameObjectWithTag("GameState");
        playerSettings = gameStateObject.GetComponent<PlayerSettings>();
        gameSettings = gameStateObject.GetComponent<GameSettings>();
        gameState = gameStateObject.GetComponent<GameState>();

        if (isLocalPlayer)
		{
            GetComponent<Move>().canUse = true;
            GetComponent<LookAround>().canUse = true;
            GetComponent<Pointer>().canUse = true;
            Cursor.lockState = CursorLockMode.Locked;

            GameObject networkManager = GameObject.FindGameObjectWithTag("GameController");
            string inputName = networkManager.GetComponent<BetweenUsNetworkManager>().playerName;
            CmdUpdatePlayerName(inputName);
            canvas.SetActive(true);
            mapCanvas.SetActive(true);
            
            GameObject.FindGameObjectWithTag("MenuController").GetComponent<Pause>().client = this.gameObject;
        }
        gameState.AddPlayersList(this.gameObject);
    }

	private void OnDestroy()
	{
        if (!isLocalPlayer)
        {
            gameState.RemovePlayersList(this.gameObject);
        }
    }

    public void OnGameStart(Role oldRole, Role newRole)
    {
        if (oldRole == Role.lobby)
		{
            if (isLocalPlayer)
            {
                gameState.OpenRoleUI(this.gameObject);
            }

            if (newRole == Role.impostor)
			{
                gameState.impostorsList.Add(this.gameObject);
			}
        }
    }

    [ClientRpc]
    public void RpcGoToMeeting()
	{
        transform.position = meetingPosition.position;
        transform.rotation = meetingPosition.rotation;
        gameObject.GetComponent<LookAround>().RefreshXRotation();
	}
    private void Update()
	{
		if (isLocalPlayer && Input.GetButtonDown("Ready") && playerRole == Role.lobby)
		{
            CmdToggleReady();
        }
	}
    [Command]
    public void CmdToggleReady()
	{
        isReady = !isReady;
        bool allReady = true;

		for (int i = 0; i < gameState.playersList.Count; i++)
		{
            if (!gameState.playersList[i].GetComponent<ClientController>().isReady)
			{
                allReady = false;
            }
		}

        if (allReady && gameState.playersList.Count > 3)
		{
            gameState.GameStart();
		}
	}
	[Command]
	public void CmdUpdatePlayerName(string inputName)
	{
        int n = Random.Range(0, 100);
        if (inputName == "")
        {
            playerName = "Player " + n;
        }
		else
		{
            playerName = inputName;
		}
	}
    
    // Tasks
    public void ActionTask(Task.Tasks type)
	{
		switch (type)
		{
			case Task.Tasks.changeSkin:
                CmdChangeSkin();
                break;
			default:
				break;
		}
	}
    [Command]
    void CmdChangeSkin()
    {
        GameObject networkManager = GameObject.FindGameObjectWithTag("GameController");
        networkManager.GetComponent<BetweenUsNetworkManager>().SelectFirstSkin(this.gameObject);
    }

    public void ChangeFloatSetting(GameSettings.Setting id, float value)
	{
        CmdChangeFloatSetting(id, value);
    }
    [Command]
    void CmdChangeFloatSetting(GameSettings.Setting id, float value)
	{
        switch (id)
        {
            case GameSettings.Setting.fogDensity:
                gameSettings.fogDensity = value;
                break;
            case GameSettings.Setting.playerSpeed:
                gameSettings.playerSpeed = value;
                break;
            case GameSettings.Setting.killDistance:
                gameSettings.killDistance = value;
                break;
            default:
                break;
        }
    }
    public void ChangeIntSetting(GameSettings.Setting id, int value)
    {
        CmdChangeIntSetting(id, value);
    }
    [Command]
    void CmdChangeIntSetting(GameSettings.Setting id, int value)
    {
        switch (id)
        {
            case GameSettings.Setting.impostorsCount:
                gameSettings.impostorsCount = value;
                break;
            case GameSettings.Setting.tasksCount:
                gameSettings.tasksCount = value;
                break;
            default:
                break;
        }
    }
    public void ResetSettings()
	{
        CmdResetSettings();
    }
    [Command]
	void CmdResetSettings()
	{
        gameSettings.Restart();
	}

    public void AddToFinishTasks()
	{
        CmdAddToFinishTasks();
    }
    [Command]
    public void CmdAddToFinishTasks()
    {
        gameState.gameObject.GetComponent<TasksState>().totalTasksDone += 1;
    }
}