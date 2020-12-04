using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuSceneController : MonoBehaviour
{
    public TMP_Text highScoreText;
    private int highScoreValue;

    public GameObject mainMenu;
    public GameObject tutorialMenu; 

    void Start()
    {
        highScoreValue = PlayerPrefs.GetInt("highScore", 0);
        if (highScoreValue > 0)
            highScoreText.text = "High Score: " + highScoreValue;
    }

    // Start Game
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Show Tutorial
    public void ShowTutorial()
    {
        tutorialMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    // Go Back to Main Menu
    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        tutorialMenu.SetActive(false);
    }

    // Close Application
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false; // Stops app from running in the editor
        Application.Quit();
    }
}
