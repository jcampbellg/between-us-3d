using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject mapPanel;
    public Settings settings;
    public Slider slider;
    public GameObject client;
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !settings.isTaskOpen && !settings.isMapOpen)
		{
            bool active = !settings.isPauseOpen;
            settings.isPauseOpen = active;
            settings.isMenuOpen = active;
            pausePanel.SetActive(active);
            if (active)
			{
                Cursor.lockState = CursorLockMode.None;
            }
            else
			{
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if ( client && ((Input.GetButtonDown("Map") && !settings.isPauseOpen) || (settings.isMapOpen && Input.GetButtonDown("Cancel"))))
        {
            bool active = !settings.isMapOpen;
            settings.isMapOpen = active;
            settings.isMenuOpen = active;
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

    public void OnMouseSensitivityChange()
    {
        settings.mouseSensitivity = slider.value;
    }

    public void ToggleFullscreen()
	{
        if (!Screen.fullScreen)
		{
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
		}
        else
		{
            Screen.fullScreen = false;
		}
	}

    public void ExitGame()
	{
        Application.Quit();
    }
}
