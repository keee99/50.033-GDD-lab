using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{

    // UnityEvents: Container of functions subscribed to the specific input action
    // Add callbacks from the inspector
    public UnityEvent jump;
    public UnityEvent jumphold;
    public UnityEvent<int> moveCheck;

    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Debug.Log("Jumphold Start");
        }
        else if (context.performed)
        {
            // Debug.Log("Jumphold performed");
            jumphold.Invoke();
        }
        else if (context.canceled)
        {
            // Debug.Log("Jumphold canceled");
        }
    }


    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Debug.Log("Jump Start");
        }
        else if (context.performed)
        {
            // Debug.Log("Jump performed");
            jump.Invoke();
        }
        else if (context.canceled)
        {
            // Debug.Log("Jump canceled");
        }
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Debug.Log("Move Start");
            int faceRight = context.ReadValue<float>() > 0 ? 1 : -1;
            moveCheck.Invoke(faceRight);
        }
        else if (context.canceled)
        {
            // Debug.Log("Move canceled");
            moveCheck.Invoke(0);
        }
    }

    public void onClickAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Debug.Log("Click Start");
        }
        else if (context.performed)
        {
            // Debug.Log("Click performed");
        }
        else if (context.canceled)
        {
            // Debug.Log("Click canceled");
        }
    }

    public void onPointAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 point = context.ReadValue<Vector2>();
            // Debug.Log($"Point detected at {point}");
        }
    }


}
