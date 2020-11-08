using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Websocket;

public class ClientController : NetworkBehaviour
{
    public enum Role
	{
        lobby,
        crew,
        impostor
	};
    public GameObject deadBody;
    public Vector3 CamAliveOffset;
    public Vector3 CamDeadOffset;
    public LayerMask ghostLayerView;
    public LayerMask playerLayerView;
    Transform meetingPosition;
    PlayerSettings playerSettings;
    GameSettings gameSettings;
    GameState gameState;
    TasksState taskState;
    public GameObject canvas;
    public GameObject mapCanvas;
    [SyncVar]
    public string playerName = "";
    [SyncVar(hook = nameof(OnRoleChange))]
    public Role playerRole = Role.lobby;
    [SyncVar]
    public bool isReady = false;
    [SyncVar]
    public int taskCount = 0;
    [SyncVar(hook = nameof(OnGhost))]
    public bool isGhost = false;

    void Start()
    {
        meetingPosition = transform;
        GameObject gameStateObject = GameObject.FindGameObjectWithTag("GameState");
        playerSettings = gameStateObject.GetComponent<PlayerSettings>();
        gameSettings = gameStateObject.GetComponent<GameSettings>();
        gameState = gameStateObject.GetComponent<GameState>();
        taskState = gameStateObject.GetComponent<TasksState>();

        if (isLocalPlayer)
		{
            playerSettings.localPlayer = this.gameObject;
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

    public void OnRoleChange(Role oldRole, Role newRole)
    {
        if (oldRole == Role.lobby)
		{
            if (isLocalPlayer)
            {
                gameState.OpenRoleUI(this.gameObject);
                taskState.AssignShortTasks();
                taskState.ActiveCommonTasks();
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

        if (allReady)
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
    
    public void KillCrew(GameObject victim)
	{
        this.GetComponent<CharacterController>().Move(victim.transform.position - transform.position);
        CmdKillCrew(victim);
    }
    [Command]
    void CmdKillCrew(GameObject victim)
	{
        victim.GetComponent<ClientController>().isGhost = true;
    }
    [Command]
    void CmdSpawnBody()
	{
        GameObject deadVictim = Instantiate(deadBody, this.transform.position, this.transform.rotation);
        deadVictim.GetComponent<SkinRenderer>().skin = this.GetComponent<SkinRenderer>().skin;
        NetworkServer.Spawn(deadVictim);
    }
    public void OnGhost(bool oldValue, bool newValue)
	{
        if (newValue)
		{
            this.gameObject.layer = LayerMask.NameToLayer("Ghost");
            this.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Ghost");

            if (isLocalPlayer)
			{
                Camera.main.cullingMask = ghostLayerView;
                Camera.main.transform.localPosition = CamDeadOffset;
                this.GetComponent<Pointer>().clickActionDistance = 6f;
            }

            this.GetComponent<Move>().hasGravity = false;
            this.GetComponent<SkinRenderer>().GhostUp();

            this.transform.position = new Vector3(transform.position.x, 0.7f, transform.position.z);
            CmdSpawnBody();
        }
        else
		{
            this.gameObject.layer = LayerMask.NameToLayer("Player");
            this.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Player");

            if (isLocalPlayer)
			{
                Camera.main.cullingMask = playerLayerView;
                Camera.main.transform.localPosition = CamAliveOffset;
                this.GetComponent<Pointer>().clickActionDistance = 2.5f;
            }

            this.GetComponent<Move>().hasGravity = true;
            this.GetComponent<SkinRenderer>().GhostUp();
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
            case GameSettings.Setting.impostorCount:
                gameSettings.impostorCount = value;
                break;
            case GameSettings.Setting.commonTaskCount:
                gameSettings.commonTaskCount = value;
                break;
            case GameSettings.Setting.shortTaskCount:
                gameSettings.shortTaskCount = value;
                break;
            case GameSettings.Setting.longTaskCount:
                gameSettings.longTaskCount = value;
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

    public void AddToTotalTaskCount(int add)
	{
        CmdAddToTotalTaskCount(add);
    }
    [Command]
    public void CmdAddToTotalTaskCount(int add)
    {
        taskCount += add;
        taskState.totalTasks += add;
    }
}