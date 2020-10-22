using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClientController : NetworkBehaviour
{
    public Settings settings;
    public GameObject canvas;
    public GameObject mapCanvas;
    [SyncVar]
    public string playerName = "";
    void Start()
    {
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
            CmdUpdateSettings();
            canvas.SetActive(true);
            mapCanvas.SetActive(true);

            GameObject.FindGameObjectWithTag("MenuController").GetComponent<Pause>().client = this.gameObject;
        }
    }
    [Command]
	public void CmdUpdatePlayerName(string inputName)
	{
        playerName = inputName;
	}

    [Command]
    public void CmdUpdateSettings()
    {
        RpcUpdateSettings(settings.fogDensity, settings.playerSpeed);
    }
    [ClientRpc]
    public void RpcUpdateSettings(float fog, float playerSpeed)
	{
        settings.UpdateSettings(fog, playerSpeed);
	}
    private void Update()
	{
        if (isLocalPlayer)
        {
            if (settings.isMenuOpen)
            {
                GetComponent<Move>().canUse = false;
                GetComponent<LookAround>().canUse = false;
            }
            else
            {
                GetComponent<Move>().canUse = true;
                GetComponent<LookAround>().canUse = true;
            }
        }
    }
    
    // Tasks
    public void ActionTask(Task.Tasks id)
	{
		switch (id)
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

    public void ChangeSetting(Settings.Setting id)
	{
		switch (id)
		{
			case Settings.Setting.fogDensity:
                CmdChangeFloatSetting(id, settings.fogDensity);
                break;
			case Settings.Setting.playerSpeed:
                CmdChangeFloatSetting(id, settings.playerSpeed);
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
                settings.ChangeFog(value);
                break;
            case Settings.Setting.playerSpeed:
                settings.playerSpeed = value;
                break;
            default:
                break;
        }
        RpcChangeFloatSetting(id, value);
    }
    [ClientRpc]
    void RpcChangeFloatSetting(Settings.Setting id, float value)
	{
        switch (id)
        {
            case Settings.Setting.fogDensity:
                settings.ChangeFog(value);
                break;
            case Settings.Setting.playerSpeed:
                settings.playerSpeed = value;
                break;
            default:
                break;
        }
    }
}