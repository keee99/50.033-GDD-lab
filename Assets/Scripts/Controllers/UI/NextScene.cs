using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public IntVariable currentWorld;

    public string nextSceneName;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentWorld.Value = currentWorld.Value + 1;
            if (currentWorld.Value > 2)
            {
                SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single);
            }
            SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
        }
    }
}
