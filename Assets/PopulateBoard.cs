using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateBoard : MonoBehaviour
{

    public GameObject boardRef;
    public GameObject tileRowRef;
    public GameObject tileRef;

    public GameObject TileRowRef { get => tileRowRef; set => tileRowRef = value; }

    // Start is called before the first frame update
    void Start()
    {
        // Get Board's position for later use
        float boardRefX = boardRef.transform.position.x;
        float boardRefY = boardRef.transform.position.y;

        //Create 7x5 Board
        for (int i = 0; i < 7; i++)
        {
            GameObject currentRow = Instantiate(tileRowRef, new Vector3(0, boardRefY + i * -0.95f, 9.5f), Quaternion.identity, boardRef.transform);
            float currentRowYPos = currentRow.transform.position.y;
            float currentRowXScale = currentRow.transform.localScale.x;
            for (int j = 0; j < 5; j++)
            {
                Instantiate(tileRef, new Vector3(boardRefX + (j * 1.2f * currentRowXScale), currentRowYPos, 9.5f), Quaternion.identity, currentRow.transform);
            }
        }
    }
}
