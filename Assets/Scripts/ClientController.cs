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
    public Settings settings;
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
        settings = GameObject.FindGameObjectWithTag("GameState").GetComponent<Settings>();
        settings.localPlayer = this.gameObject;
        gameObject.GetComponent<Move>().settings = settings;
        gameObject.GetComponent<LookAround>().settings = settings;
        gameObject.GetComponent<Pointer>().settings = settings;
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
        settings.AddPlayersList(this.gameObject);
    }

	private void OnDestroy()
	{
        if (!isLocalPlayer)
        {
            settings.RemovePlayersList(this.gameObject);
        }
    }

    public void OnGameStart(Role oldRole, Role newRole)
    {
        if (oldRole == Role.lobby)
		{
            if (isLocalPlayer)
            {
                settings.OpenRoleUI(this.gameObject);
            }

            if (newRole == Role.impostor)
			{
                settings.impostorsList.Add(this.gameObject);
			}
        }
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

		for (int i = 0; i < settings.playersList.Count; i++)
		{
            if (!settings.playersList[i].GetComponent<ClientController>().isReady)
			{
                allReady = false;
            }
		}

        if (allReady && settings.playersList.Count > 3)
		{
            settings.GameStart();
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

    public void ChangeFloatSetting(Settings.Setting id, float value)
	{
        CmdChangeFloatSetting(id, value);
    }
    [Command]
    void CmdChangeFloatSetting(Settings.Setting id, float value)
	{
        switch (id)
        {
            case Settings.Setting.fogDensity:
                settings.fogDensity = value;
                break;
            case Settings.Setting.playerSpeed:
                settings.playerSpeed = value;
                break;
            case Settings.Setting.killDistance:
                settings.killDistance = value;
                break;
            default:
                break;
        }
    }
    public void ChangeIntSetting(Settings.Setting id, int value)
    {
        CmdChangeIntSetting(id, value);
    }
    [Command]
    void CmdChangeIntSetting(Settings.Setting id, int value)
    {
        switch (id)
        {
            case Settings.Setting.impostorsCount:
                settings.impostorsCount = value;
                break;
            case Settings.Setting.tasksCount:
                settings.tasksCount = value;
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
        settings.Restart();
	}

    public void AddToFinishTasks()
	{
        CmdAddToFinishTasks();
    }
    [Command]
    public void CmdAddToFinishTasks()
    {
        settings.gameObject.GetComponent<TasksState>().totalTasksDone += 1;
    }
}