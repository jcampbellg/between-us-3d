using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings")]
public class Settings : ScriptableObject
{
    public float mouseSensitivity;
    public bool isMenuOpen;
    public bool isPauseOpen;
    public bool isTaskOpen;
}