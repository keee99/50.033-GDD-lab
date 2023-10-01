using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public void ResetObstacles()
    {
        ResetBlocks();
    }

    public void ResetBlocks()
    {

        GameObject blocks = GameObject.Find("Blocks");
        foreach (Transform child in blocks.transform)
        {
            child.GetComponentInChildren<BlockBehaviour>().Reset();

        }
    }

}