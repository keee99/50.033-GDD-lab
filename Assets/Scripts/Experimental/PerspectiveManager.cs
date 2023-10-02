using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PerspectiveManager : MonoBehaviour
{
    public UnityEvent flipPerspectiveTo3D;
    public UnityEvent flipPerspectiveTo2D;

    public static PerspectiveManager Instance;

    public bool is2D = true;
    private bool isPressed = false;

    // Singleton Management
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void FlipTo2D()
    {
        flipPerspectiveTo2D.Invoke();
    }

    public void FlipTo3D()
    {
        flipPerspectiveTo3D.Invoke();
    }

    public void Toggle(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressed = true;
        }
        else if (context.canceled && isPressed)
        {
            Debug.Log("toggle");
            if (is2D)
            {
                FlipTo3D();
            }
            else
            {
                FlipTo2D();
            }
            is2D = !is2D;
        }

    }



}
