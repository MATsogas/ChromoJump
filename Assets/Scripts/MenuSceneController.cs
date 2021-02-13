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
    public GameObject creditsMenu;

    private Button nextPageButton;
    private Button previousPageButton;

    void Start()
    {
        // Get saved High Scored
        highScoreValue = PlayerPrefs.GetInt("highScore", 0);
        if (highScoreValue > 0)
            highScoreText.text = "High Score: " + highScoreValue;

        // Initialise Tutorial Navigation Buttons
        nextPageButton = tutorialMenu.transform.Find("Next Button").gameObject.GetComponent<Button>();
        previousPageButton = tutorialMenu.transform.Find("Previous Button").gameObject.GetComponent<Button>();
    }

    // ---- Button OnClick Functions ----

    // -------- MAIN MENU --------

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
        // Reset Nav buttons
        nextPageButton.gameObject.SetActive(true);
        previousPageButton.gameObject.SetActive(false);
    }

    // Show Credits
    public void ShowCredits()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    // Close Application
    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false; // Stops app from running in the editor
        Application.Quit();
    }

    // ---------- TUTORIAL ----------

    // Go Back to Main Menu
    public void BackToMainMenuTutorial()
    {
        mainMenu.SetActive(true);
        tutorialMenu.SetActive(false);
    }

    // Switch to next tutorial page
    public void NextPageTutorial()
    {
        previousPageButton.gameObject.SetActive(true);
        nextPageButton.gameObject.SetActive(tutorialMenu.GetComponent<PageHandler>().NextPage());
    }

    // Switch to previous tutorial page
    public void PreviousPageTutorial ()
    {
        nextPageButton.gameObject.SetActive(true);
        previousPageButton.gameObject.SetActive(tutorialMenu.GetComponent<PageHandler>().PreviousPage());
    }

    // ---------- CREDITS ----------

    // Go Back to Main Menu
    public void BackToMainMenuCredits()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    // Open Song Link on System's Default Browser
    public void OpenSongLink()
    {
        Application.OpenURL("https://incompetech.filmmusic.io/song/3953-killing-time");
    }

    // Open Song License Link on System's Default Browser
    public void OpenLicenseLink()
    {
        Application.OpenURL("https://filmmusic.io/standard-license");
    }
}
