using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAndWait : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public IntVariable currentWorld;

    void Start()
    {
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        for (float alpha = 1f; alpha >= -0.05f; alpha -= 0.05f)
        {
            canvasGroup.alpha = alpha;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        string level = "1-" + currentWorld.Value;
        // once done, go to next scene
        SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
    }

    public void ReturnToMain()
    {
        // TODO
        Debug.Log("Return to main menu");
    }
}
