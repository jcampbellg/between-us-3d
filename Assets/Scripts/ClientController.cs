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
    [SyncVar]
    public Role playerRole = Role.lobby;

    void Start()
    {
        settings = GameObject.FindGameObjectWithTag("GameState").GetComponent<Settings>();
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
            string inputName = networkManager.GetComponent<BeetweenUsNetworkManager>().playerName;

            if ( inputName != "")
			{
                CmdUpdatePlayerName(inputName);
            }
            canvas.SetActive(true);
            mapCanvas.SetActive(true);
            gameObject.tag = "LocalPlayer";

            GameObject.FindGameObjectWithTag("MenuController").GetComponent<Pause>().client = this.gameObject;
        }
    }
    [Command]
	public void CmdUpdatePlayerName(string inputName)
	{
        playerName = inputName;
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
        networkManager.GetComponent<BeetweenUsNetworkManager>().SelectFirstSkin(this.gameObject);
    }

    public void ChangeFloatSetting(Settings.Setting id, float value)
	{
		switch (id)
		{
			case Settings.Setting.fogDensity:
			case Settings.Setting.playerSpeed:
            case Settings.Setting.killDistance:
                CmdChangeFloatSetting(id, value);
                break;
            default:
				break;
		}
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
        switch (id)
        {
            case Settings.Setting.impostorsCount:
                CmdChangeIntSetting(id, value);
                break;
            default:
                break;
        }
    }
    [Command]
    void CmdChangeIntSetting(Settings.Setting id, int value)
    {
        switch (id)
        {
            case Settings.Setting.impostorsCount:
                settings.impostorsCount = value;
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
}