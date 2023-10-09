using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void ButtonClick()
    {
        Debug.Log("bruh");
        GameManager.Instance.Reset();
    }
}
