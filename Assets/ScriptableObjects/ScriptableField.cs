using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidData", menuName = "ScriptableObjects/SpawnFieldScriptableObject", order = 2)]
public class ScriptableField : ScriptableObject
{
    public float FieldSizeVertical;
    public float FieldSizeHorizontal;
}
