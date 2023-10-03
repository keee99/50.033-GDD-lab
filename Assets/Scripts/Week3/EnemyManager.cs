using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public void ResetEnemies()
    {
        foreach (Transform child in transform)
        {
            child.GetChild(0).GetComponentInChildren<EnemyMovement>().Reset();
            child.GetChild(0).GetComponentInChildren<EnemyDeath>().Reset();

            child.GetChild(1).GetComponentInChildren<EnemyMovement3D>().Reset();
            child.GetChild(1).GetComponentInChildren<EnemyDeath3D>().Reset();
        }
    }

    public void FlipEnemies(bool to3D)
    {
        foreach (EnemyFlippable enemy in GetComponentsInChildren<EnemyFlippable>())
        {
            enemy.Flip(to3D);
        }
    }
}
