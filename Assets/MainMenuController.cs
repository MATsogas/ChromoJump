using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
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
