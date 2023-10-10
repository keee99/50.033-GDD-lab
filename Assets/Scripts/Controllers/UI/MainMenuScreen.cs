using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    public IntVariable score;

    public TextMeshProUGUI hiScoreText;

    // Start is called before the first frame update
    void Start()
    {
        hiScoreText.text = "HI-SCORE: " + score.previousHighestValue.ToString("D6");
    }

}