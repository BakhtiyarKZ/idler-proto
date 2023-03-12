using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "SO/Settings")]
public class SettingsSO : ScriptableObject
{
    [Tooltip("Maximum amount player can stack in back")]
    public int maxBlockCount;

    [Tooltip("Time the wheat need to grow")]
    public float wheatGrowDelay;
}
