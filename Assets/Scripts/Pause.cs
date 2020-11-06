using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject mapPanel;
    public PlayerSettings playerSettings;
    public GameObject client;
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !playerSettings.isTaskOpen && !playerSettings.isMapOpen)
		{
            bool active = !playerSettings.isPauseOpen;
            playerSettings.isPauseOpen = active;
            playerSettings.isMenuOpen = active;
            pausePanel.SetActive(active);
            if (active)
			{
                Cursor.lockState = CursorLockMode.None;
            }
            else
			{
                if (client)
                    Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if ( client && ((Input.GetButtonDown("Map") && !playerSettings.isPauseOpen) || (playerSettings.isMapOpen && Input.GetButtonDown("Cancel"))))
        {
            bool active = !playerSettings.isMapOpen;
            playerSettings.isMapOpen = active;
            playerSettings.isMenuOpen = active;
            if (!mapPanel.GetComponent<MapUI>().client)
			{
                mapPanel.GetComponent<MapUI>().client = client;
			}
            mapPanel.SetActive(active);
            if (active)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void OnMouseSensitivityChange(float value)
    {
        playerSettings.mouseSensitivity = value;
    }

    public void ToggleFullscreen()
	{
        if (Screen.fullScreenMode == FullScreenMode.Windowed || Screen.fullScreenMode == FullScreenMode.MaximizedWindow)
		{
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
        }
        else
		{
            Screen.SetResolution(1024, 769, FullScreenMode.Windowed);
        }
	}

    public void ExitGame()
	{
        Application.Quit();
    }
}
