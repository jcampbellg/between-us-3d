﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Client : NetworkBehaviour
{
    public Settings settings;
    public GameObject canvas;
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
                CmdUpdateSettings();
			}
            canvas.SetActive(true);
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
        RpcUpdateSettings(settings.fogDistance);
    }
    [ClientRpc]
    public void RpcUpdateSettings(float fog)
	{
        settings.UpdateSettings(fog);
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
    
    public void ChangeSkin()
	{
        CmdChangeSkin();
    }
    [Command]
    void CmdChangeSkin()
    {
        GameObject networkManager = GameObject.FindGameObjectWithTag("GameController");
        networkManager.GetComponent<BeetweenUsNetworkManager>().SelectFirstSkin(this.gameObject);
    }

    public void ChangeFog(float newFog)
	{
        CmdChangeFog(newFog);

    }
    [Command]
    void CmdChangeFog(float newFog)
	{
        RpcChangeFog(newFog);
        settings.ChangeFog(newFog);
    }
    [ClientRpc]
    void RpcChangeFog(float newFog)
	{
        settings.ChangeFog(newFog);
    }
}