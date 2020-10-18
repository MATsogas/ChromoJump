using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public GameObject tileRow;
    public GameObject tile;
    public GameObject player;

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
    }

    public int AwardScoreCalculator(Color playerCoreColor, Color tileColor)
    {
        // Change colors to Vector3s
        Vector3 playerCoreV3 = (Vector4)(playerCoreColor);
        Vector3 tileV3 = (Vector4)(tileColor);
        // Calculate distance and return the resulting score
        float distance = Mathf.Abs(Vector3.Distance(playerCoreV3, tileV3));
        int scoreAwarded = Mathf.RoundToInt(200 * (Mathf.Sqrt(3) - distance) / Mathf.Sqrt(3)) * 5;
        Debug.Log(scoreAwarded);
        return scoreAwarded;
    }
}
