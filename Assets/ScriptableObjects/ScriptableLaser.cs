using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaserData", menuName = "ScriptableObjects/SpawnLaserScriptableObject", order = 4)]
public class ScriptableLaser : ScriptableObject
{
    public GameObject LaserPrefab;
    public float LaserRadius;
    public float LaserFadeTime;
}
