using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public void ResetEnemies()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<EnemyMovement>().Reset();
        }
    }
}
