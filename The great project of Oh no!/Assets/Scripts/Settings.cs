using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings")]
public class Settings : ScriptableObject
{
    public int population;
    public float generalVolume;
    public float soundVolume;
    public float musicVolume;
    public float mouseSensitivity;
    public float FOV;
}
