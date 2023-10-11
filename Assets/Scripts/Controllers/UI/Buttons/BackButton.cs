using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void ButtonClick()
    {
        // TODO: Call a reset here
        // Reset timescale, mixer
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single);
    }

}
