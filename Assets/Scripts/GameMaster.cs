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
    public int rows = 7;
    public int columns = 5;
    public float tilesOffset = 1;

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

        //Find Edges
        float topEdge = tilesOffset * (rows / 2f - 0.5f);
        float bottomEdge = topEdge * -1;
        float rightEdge = tilesOffset * (columns / 2f - 0.5f);
        float leftEdge = rightEdge * -1;

        // Create Board
        for (float curRow = bottomEdge; curRow <= topEdge; curRow+=tilesOffset)
        {
            GameObject currentRow = Instantiate(tileRow, gameObject.transform);
            currentRow.transform.localPosition = new Vector3(0, curRow, 0);
            for (float curTile = leftEdge; curTile <= rightEdge; curTile+=tilesOffset)
            {
                GameObject currentTile = Instantiate(tile, currentRow.transform);
                currentTile.transform.localPosition = new Vector3(curTile, 0, 9.5f);
            }
        }

        // Spawn Player in center of the Board
        Instantiate(player, gameObject.transform);

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
