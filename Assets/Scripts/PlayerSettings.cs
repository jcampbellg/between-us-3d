using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSettings : NetworkBehaviour
{
    public GameObject localPlayer;
    public float mouseSensitivity;
    public bool isMenuOpen;
    public bool isPauseOpen;
    public bool isTaskOpen;
    public bool isMapOpen;
}