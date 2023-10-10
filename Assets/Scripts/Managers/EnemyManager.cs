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
        }
    }

    void Awake()
    {
        GameManager.Instance.gameRestart.AddListener(ResetEnemies);
    }
}
