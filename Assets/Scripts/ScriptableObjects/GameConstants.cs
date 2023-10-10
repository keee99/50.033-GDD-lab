using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    // lives
    public int maxLives; // 99

    // Mario's movement
    public int speed; // 150
    public int maxSpeed; // 5
    public int upSpeed; // 40
    public int deathImpulse; // 60
    // public Vector3 marioStartingPosition;

    // Goomba's movement
    public float goombaPatrolTime; // 3.0f
    public float goombaMaxOffset; // 5f
}
