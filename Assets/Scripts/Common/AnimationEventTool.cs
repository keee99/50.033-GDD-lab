using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventTool : MonoBehaviour
{
    public UnityEvent triggerEvent;

    public void TriggerEvent()
    {
        triggerEvent.Invoke();
    }
}
