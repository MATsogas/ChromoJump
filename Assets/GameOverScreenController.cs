using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreenController : MonoBehaviour
{
    public Text score;

    void Start()
    {
        int gameScore = PlayerPrefs.GetInt("gameScore");
        score.text = "Score: " + gameScore;
        int highScore = PlayerPrefs.GetInt("highScore");
        if (highScore < gameScore)
        {
            // New High Score!
            PlayerPrefs.SetInt("highScore", gameScore);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
