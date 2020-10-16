using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pauseCanvas;
    public Settings settings;
    public Slider slider;
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !settings.isTaskOpen)
		{
            bool active = !settings.isPauseOpen;
            settings.isPauseOpen = active;
            settings.isMenuOpen = active;
            pauseCanvas.SetActive(active);
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

    public void ExitGame()
	{
        Application.Quit();
    }
}
