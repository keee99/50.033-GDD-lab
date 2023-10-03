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

    public UnityEvent<int> moveYCheck;

    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
        }
        else if (context.performed)
        {
            jumphold.Invoke();
        }
        else if (context.canceled)
        {
        }
    }


    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
        }
        else if (context.performed)
        {
            jump.Invoke();
        }
        else if (context.canceled)
        {
        }
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int faceRight = context.ReadValue<float>() > 0 ? 1 : -1;
            moveCheck.Invoke(faceRight);
        }
        else if (context.canceled)
        {
            moveCheck.Invoke(0);
        }
    }

    public void OnMoveYAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int moving = context.ReadValue<float>() > 0 ? 1 : -1;
            moveYCheck.Invoke(moving);
        }
        else if (context.canceled)
        {
            moveYCheck.Invoke(0);
        }
    }

    public void onClickAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
        }
        else if (context.performed)
        {
        }
        else if (context.canceled)
        {
        }
    }

    public void onPointAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 point = context.ReadValue<Vector2>();
        }
    }


}
