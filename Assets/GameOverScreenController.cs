using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreenController : MonoBehaviour
{
    public Text score;

    public void Start()
    {
        score.text = "Score: " + PlayerPrefs.GetInt("gameScore");
    }

    public void Replay()
    {
        SceneManager.LoadScene("GameScene");
    }
}
