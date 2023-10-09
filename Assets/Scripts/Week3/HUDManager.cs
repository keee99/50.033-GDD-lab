using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{

    public Canvas gameCanvas;
    public Canvas gameOverOverlay;

    // Restart Game related
    public TextMeshProUGUI overlayScoreText;
    public TextMeshProUGUI scoreText;

    public void SetScore(int score)
    {
        string scoreString = "Score: " + score.ToString();
        overlayScoreText.text = scoreString;
        scoreText.text = scoreString;
    }

    public void GameStart()
    {
        toggleGameOverUI(false);
    }

    public void GameOver()
    {
        toggleGameOverUI(true);
    }

    private void toggleGameOverUI(bool toggle)
    {
        gameOverOverlay.gameObject.SetActive(toggle);

    }

    void Awake()
    {
        GameManager.Instance.gameRestart.AddListener(GameStart);
        GameManager.Instance.gameStart.AddListener(GameStart);
        GameManager.Instance.gameOver.AddListener(GameOver);
        GameManager.Instance.scoreChange.AddListener(SetScore);
    }
}
