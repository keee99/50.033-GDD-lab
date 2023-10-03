using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    GameObject blocks;

    private void Start()
    {
        blocks = GameObject.Find("Blocks");
    }

    public void ResetObstacles()
    {
        ResetBlocks();
    }

    public void ResetBlocks()
    {
        foreach (Transform child in blocks.transform)
        {
            BlockBehaviour blockB = child.GetComponentInChildren<BlockBehaviour>();
            if (blockB != null)
            {
                blockB.Reset();
            }

        }
    }

    public void FlipBlocks(bool to3D)
    {
        foreach (Flippable flippable in blocks.GetComponentsInChildren<Flippable>())
        {
            flippable.Flip(to3D);
        }
    }

}