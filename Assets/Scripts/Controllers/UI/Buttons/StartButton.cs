using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public IntVariable currentWorld;

    public IntVariable score;

    public void ButtonClick()
    {
        ResetScore();
        currentWorld.Value = 1;
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
    }

    public void ResetScore()
    {
        score.Value = 0;
    }
}
