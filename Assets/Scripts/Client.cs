using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Client : NetworkBehaviour
{
    public Settings settings;
    void Start()
    {
        if (isLocalPlayer)
		{
            GetComponent<Move>().canUse = true;
            GetComponent<LookAround>().canUse = true;
            GetComponent<Pointer>().canUse = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
	private void Update()
	{
        if (isLocalPlayer && Input.GetButtonDown("Cancel"))
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
}