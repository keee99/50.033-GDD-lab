using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetHSButton : MonoBehaviour
{
    public IntVariable score;
    public TextMeshProUGUI hiscoreText;

    public void ButtonClick()
    {
        GetComponent<AudioSource>().Play();
        score.ResetHighestValue();
        hiscoreText.text = "HI-SCORE: " + score.previousHighestValue.ToString("D6");
        GameObject eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

}
