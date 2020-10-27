using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TMP_Text highScore;
    private int highScoreValue;

    void Start()
    {
        highScoreValue = PlayerPrefs.GetInt("highScore", 0);
        if (highScoreValue > 0)
            highScore.text = "High Score: " + highScoreValue;
    }

    // Start Game
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Close Application
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false; // Stops app from running in the editor
        Application.Quit();
    }
}
