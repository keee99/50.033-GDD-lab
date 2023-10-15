using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestartButton : MonoBehaviour, IInteractiveButton
{
    public UnityEvent gameRestart;
    public void ButtonClick()
    {
        gameRestart.Invoke();
        Debug.Log("ASD");
    }
}
