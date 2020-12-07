using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameMaster : MonoBehaviour
{

    public GameObject tileRow;
    public GameObject tile;
    public GameObject player;
    public TMP_Text score;
    public TMP_Text moves;

    private int curScore;
    private int curMoves;

    // Start is called before the first frame update
    void Start()
    {
        // Get Board's position for later use
        float boardPosX = gameObject.transform.position.x;
        float boardPosY = gameObject.transform.position.y;

        // Get Rows' scale on X axis for later use
        float rowScaleX = tileRow.transform.localScale.x;

        // Create 7x5 Board
        for (int i = 0; i < 7; i++)
        {
            GameObject currentRow = Instantiate(tileRow, new Vector3(0, boardPosY + i * -0.95f, 9.5f), Quaternion.identity, gameObject.transform);
            float currentRowYPos = currentRow.transform.position.y;
            for (int j = 0; j < 5; j++)
            {
                Instantiate(tile, new Vector3(boardPosX + (j * 1.2f * rowScaleX), currentRowYPos, 9.5f), Quaternion.identity, currentRow.transform);
            }
        }

        // Spawn Player in center tile of 7x5 Board
        Instantiate(player, new Vector3(boardPosX + (2 * 1.2f * rowScaleX), boardPosY + (3 * -0.95f), 9f), Quaternion.Euler(new Vector3(90, 0, 0)), gameObject.transform);

        // Set score to 0
        curScore = 0;
        SetScore(curScore);

        // Set moves to 12
        curMoves = 12;
        SetMoves(curMoves);
    }

    private int AwardScoreCalculator(Color playerCoreColor, Color tileColor)
    {
        // Change colors to Vector3s
        Vector3 playerCoreV3 = (Vector4)(playerCoreColor);
        Vector3 tileV3 = (Vector4)(tileColor);
        // Calculate distance and return the resulting score
        float distance = Mathf.Abs(Vector3.Distance(playerCoreV3, tileV3));
        int scoreAwarded = Mathf.RoundToInt(200 * (Mathf.Sqrt(3) - distance) / Mathf.Sqrt(3)) * 5;
        return scoreAwarded;
    }

    public void MoveMade(Color playerCoreColor, Color tileColor)
    {
        int score = AwardScoreCalculator(playerCoreColor, tileColor);
        int mpChange = 0;

        if (score < 620) // POOR (No score, Lose MP)
        {
            curScore += 0;
            mpChange = -1;
        } else if (score < 741)
        {
            curScore += score / 2;
            mpChange = -1;
        } else if (score < 856) // GOOD (Full score, Lose MP)
        {
            curScore += score;
            mpChange = -1;
        } else if (score < 990) // GREAT (Full score, No MP loss)
        {
            curScore += score;
            mpChange = 0;
        } else if (score < 1001) // PERFECT (Full score, Gain MP)
        {
            curScore += score;
            mpChange = 1;
        } 

        SetScore(curScore);

        curMoves += mpChange;
        if (curMoves > 0)
        {
            SetMoves(curMoves);
        } else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        PlayerPrefs.SetInt("gameScore", curScore);
        SceneManager.LoadScene("GameOverScreenScene");
    }

    private void SetScore(int newScore)
    {
        score.text = "Score: " + newScore;
    }

    private void SetMoves(int newMoves)
    {
        moves.text = "Moves Left: " + newMoves;
    }
}
