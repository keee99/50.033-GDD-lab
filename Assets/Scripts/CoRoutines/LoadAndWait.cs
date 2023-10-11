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
            yield return new WaitForSecondsRealtime(0.16f);
        }

        string level = "1-" + currentWorld.Value;

        int sceneIndex = currentWorld.Value;
        if (sceneIndex > 2)
        {
            sceneIndex %= 2;
            sceneIndex = sceneIndex == 0 ? 2 : 1;
        }
        // once done, go to next scene
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
    }

    public void ReturnToMain()
    {
        // TODO
        Debug.Log("Return to main menu");
    }
}
