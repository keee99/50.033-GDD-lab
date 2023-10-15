using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{

    public UnityEvent gameRestart;

    public void ButtonClick()
    {
        // TODO: Call a reset here
        // Reset timescale, mixer
        gameRestart.Invoke();
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single);
    }

}
