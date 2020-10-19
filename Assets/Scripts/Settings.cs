using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings")]
public class Settings : ScriptableObject
{
    public enum Setting
	{
        fogDensity,
        playerSpeed
	};
    public float mouseSensitivity;
    public bool isMenuOpen;
    public bool isPauseOpen;
    public bool isTaskOpen;
    public float fogDensity = 0.09f;
    public float clickPlayerDistance = 17.0f;
    public float playerSpeed = 10.0f;

    public void Restart()
	{
        mouseSensitivity = 800f;
        isMenuOpen = false;
        isPauseOpen = false;
        isTaskOpen = false;

        ChangeFog(0.09f);
        playerSpeed = 10.0f;
    }

    public void OpenTask()
	{
        isMenuOpen = true;
        isTaskOpen = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void CloseTask()
    {
        isMenuOpen = false;
        isTaskOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ChangeFog(float newFog)
	{
        fogDensity = newFog;
        RenderSettings.fogDensity = newFog;
        clickPlayerDistance = ((0.4f - newFog) / 0.4f * 13f)+2.5f;
    }
    public void UpdateSettings(float _fogDistance, float _playerSpeed)
	{
        ChangeFog(_fogDistance);
        playerSpeed = _playerSpeed;
    }
}