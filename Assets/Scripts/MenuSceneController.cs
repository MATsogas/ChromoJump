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

    private GameObject pages;
    private Button nextPageButton;
    private Button previousPageButton;

    void Start()
    {
        // Get saved High Scored
        highScoreValue = PlayerPrefs.GetInt("highScore", 0);
        if (highScoreValue > 0)
            highScoreText.text = "High Score: " + highScoreValue;

        // Initialise Pages
        pages = tutorialMenu.transform.Find("Pages").gameObject;
        nextPageButton = tutorialMenu.transform.Find("Next Button").gameObject.GetComponent<Button>();
        previousPageButton = tutorialMenu.transform.Find("Previous Button").gameObject.GetComponent<Button>();
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
        //UnityEditor.EditorApplication.isPlaying = false; // Stops app from running in the editor
        Application.Quit();
    }

    // Switch to next tutorial page
    public void NextPageTutorial()
    {
        previousPageButton.gameObject.SetActive(true);
        nextPageButton.gameObject.SetActive(pages.GetComponent<PageHandler>().NextPage());
    }

    // Switch to previous tutorial page
    public void PreviousPageTutorial ()
    {
        nextPageButton.gameObject.SetActive(true);
        previousPageButton.gameObject.SetActive(pages.GetComponent<PageHandler>().PreviousPage());
    }
}
