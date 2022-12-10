using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidData", menuName = "ScriptableObjects/SpawnAsteroidScriptableObject", order = 1)]
public class ScriptableAsteroid : ScriptableObject
{
    public float AsteroidRadius;
    public float SpeedMin;
    public float SpeedMax;
    public int AmountPerWave;
}
