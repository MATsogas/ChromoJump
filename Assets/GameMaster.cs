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

    public void ChangeTileColor()
    {
        Debug.Log("Changing tile color");
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.up, out hit))
            if (hit.collider.gameObject.GetComponent<RandomColor>() != null) {
                hit.collider.gameObject.GetComponent<RandomColor>().PaintTileRandomColor();
            } else
            {
                Debug.Log("Tile not found");
            }
    }
}
