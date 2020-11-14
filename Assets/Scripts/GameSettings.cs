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
        killCooldown,
        impostorCount,
        commonTaskCount,
        shortTaskCount,
        longTaskCount
    };

    [SyncVar(hook = nameof(HookChangeFog))]
    public float fogDensity = 0.09f;
    [SyncVar]
    public float clickPlayerDistance = 17.0f;
    [SyncVar]
    public float playerSpeed = 10.0f;
    [SyncVar]
    public int commonTaskCount = 2;
    [SyncVar]
    public int shortTaskCount = 2;
    [SyncVar]
    public int longTaskCount = 1;
    [SyncVar]
    public float killDistance = 0.5f;
    [SyncVar]
    public int impostorCount = 1;
    [SyncVar]
    public int killCooldown = 15;

    public void Restart()
	{
        ChangeFog(0.09f);
        playerSpeed = 10.0f;
        commonTaskCount = 2;
        shortTaskCount = 2;
        longTaskCount = 1;
        killDistance = 0.5f;
        impostorCount = 1;
        killCooldown = 15;
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
}