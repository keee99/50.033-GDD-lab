using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public IntVariable currentWorld;
    public IntVariable score;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiScoreText;
    public TextMeshProUGUI worldText;
    public TextMeshProUGUI worldTextSmall;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE: " + score.Value;
        hiScoreText.text = "HI-SCORE: " + score.previousHighestValue.ToString("D6");
        worldText.text = "WORLD: 1-" + currentWorld.Value;
        worldTextSmall.text = "1-" + currentWorld.Value;
    }

}
