using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameSettings : NetworkBehaviour
{
    public enum Setting
	{
        fogDensity,
        playerSpeed,
        killDistance,
        impostorsCount,
        tasksCount
    };

    [SyncVar(hook = nameof(HookChangeFog))]
    public float fogDensity = 0.09f;
    [SyncVar]
    public float clickPlayerDistance = 17.0f;
    [SyncVar]
    public float playerSpeed = 10.0f;
    [SyncVar]
    public int tasksCount = 2;
    [SyncVar]
    public float killDistance = 0.5f;
    [SyncVar]
    public int impostorsCount = 1;

    public void Restart()
	{
        ChangeFog(0.09f);
        playerSpeed = 10.0f;
        tasksCount = 2;
        killDistance = 0.5f;
        impostorsCount = 1;
    }
    public void HookChangeFog(float oldFog, float newFog)
	{
        ChangeFog(newFog);
	}
    public void ChangeFog(float newFog)
	{
        fogDensity = newFog;
        RenderSettings.fogDensity = newFog;
        clickPlayerDistance = ((0.4f - newFog) / 0.4f * 13f)+2.5f;
    }
    public void UpdateSettings(float _fogDistance, float _playerSpeed, float _killDistance)
	{
        ChangeFog(_fogDistance);
        playerSpeed = _playerSpeed;
        killDistance = _killDistance;
    }
}