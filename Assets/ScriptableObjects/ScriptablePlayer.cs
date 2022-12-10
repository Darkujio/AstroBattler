using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/SpawnPlayerScriptableObject", order = 3)]
public class ScriptablePlayer : ScriptableObject
{
    public GameObject PlayerPrefab;
    public float PlayerHitRadius;
    public int MaxLaserCharges;
    public float MaxLaserCooldown;
    public float StaticSpeedDecrease;
    public float Acceleration;
}
