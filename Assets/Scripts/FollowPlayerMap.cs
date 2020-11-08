using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerMap : MonoBehaviour
{
    public PlayerSettings playerSettings;
    public GameObject mapCamera;
    void Update()
    {
        GameObject client = playerSettings.localPlayer;
        if (playerSettings && !playerSettings.isMenuOpen && client)
		{
            mapCamera.transform.position = new Vector3(client.transform.position.x, mapCamera.transform.position.y, client.transform.position.z);
        }
    }
}
