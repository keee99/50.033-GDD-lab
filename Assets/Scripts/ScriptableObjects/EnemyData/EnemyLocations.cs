using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyLocations", menuName = "ScriptableObjects/EnemyLocations", order = 1)]
public class EnemyLocations : ScriptableObject
{
    // lives
    public Vector2[] goombaLocations;
}
