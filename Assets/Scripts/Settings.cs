using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

[System.Serializable]
public class SyncListPlayers : SyncList<GameObject> { };

public class Settings : NetworkBehaviour
{
    public enum Setting
	{
        fogDensity,
        playerSpeed,
        killDistance,
        impostorsCount
    };
    public float mouseSensitivity;
    public bool isMenuOpen;
    public bool isPauseOpen;
    public bool isTaskOpen;
    public bool isMapOpen;

    public SyncListPlayers playersList = new SyncListPlayers();
    public GameObject namesUI;

    [SyncVar(hook = nameof(HookChangeFog))]
    public float fogDensity = 0.09f;
    [SyncVar]
    public float clickPlayerDistance = 17.0f;
    [SyncVar]
    public float playerSpeed = 10.0f;
    [SyncVar]
    public float killDistance = 0.5f;
    [SyncVar]
    public int impostorsCount = 1;

    void Start()
    {
        playersList.Callback += OnPlayersUpdated;
    }

    void OnPlayersUpdated(SyncListPlayers.Operation op, int index, GameObject oldItem, GameObject newItem)
	{
        switch (op)
		{
			case SyncList<GameObject>.Operation.OP_ADD:
			case SyncList<GameObject>.Operation.OP_CLEAR:
			case SyncList<GameObject>.Operation.OP_INSERT:
			case SyncList<GameObject>.Operation.OP_REMOVEAT:
			case SyncList<GameObject>.Operation.OP_SET:
			default:
                for (int i = 0; i < namesUI.transform.childCount; i++)
                {
                    if (playersList.Count > i)
					{
                        Debug.Log(i);
                        namesUI.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else
					{
                        namesUI.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
                break;
		}
	}

    public void Restart()
	{
        ChangeFog(0.09f);
        playerSpeed = 10.0f;
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