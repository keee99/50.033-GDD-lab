using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public void ResetItems()
    {
        foreach (Transform child in transform)
        {
            foreach (Transform item in child.transform)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}
