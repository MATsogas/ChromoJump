﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float playerSpeed;
    
    private GameObject playerCore;

    private Queue<GameObject> movementQueue = new Queue<GameObject>(); // In case the user clicks on another tile before the player lands
    private GameObject tileToMoveTo;

    private Vector3 lastPlayerPosition = new Vector3(); // Used to detect player motion

    // Start is called before the first frame update
    void Start()
    {
        playerCore = gameObject.transform.Find("Player Core").gameObject; // Find player's core (child GameObject named "Player Core")
        CorePainter();
        lastPlayerPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Add on a queue the tiles that are clicked
        if (Input.GetButtonDown("Fire1"))
        {
            // Bit shift the index of the "Tiles" layer (layer 8) to get a bit mask
            int tilesLayerMask = 1 << 8;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, tilesLayerMask)) 
            {
                bool isPlayerMoving = lastPlayerPosition == gameObject.transform.position;
                bool isPlayerOnTopOfTile = !isPlayerMoving && (Vector2.Distance(hit.collider.gameObject.transform.position, gameObject.transform.position) < 0.01); // Player is "standing" on tile if it's motionless AND close enough to said tile
                if (isPlayerMoving || isPlayerOnTopOfTile)
                    movementQueue.Enqueue(hit.collider.gameObject);
            }
        }

        // If queue is not empty and we are currently not moving to a tile, dequeue tile so we can start moving towards it
        if (movementQueue.Any() && tileToMoveTo == null)
        {
            tileToMoveTo = movementQueue.Dequeue();
            // Debug.Log("Moving to " + tileToMoveTo.transform.position);
        }

        // Move to next tile if available
        if (tileToMoveTo != null)
        {
            if (Vector2.Distance(tileToMoveTo.transform.position, transform.position)>=0.01) // Player is too far from tile (a.k.a. player hasn't landed on a tile yet)
            {
                // Move towards tile
                float step = playerSpeed * Time.deltaTime;
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, tileToMoveTo.transform.position, step);
            } else // Player landed on tile
            {
                // Find the colors of the player's core and the tile we landed on
                Color tileLandedColor = tileToMoveTo.GetComponent<Renderer>().material.GetColor("_Color");
                Color playerCoreColor = playerCore.GetComponent<Renderer>().material.GetColor("_Color");

                gameObject.transform.parent.GetComponent<GameMaster>().AwardScoreCalculator(playerCoreColor, tileLandedColor); // Update score

                playerCore.GetComponent<Renderer>().material.color = tileToMoveTo.GetComponent<Renderer>().material.GetColor("_Color"); // Change the color of the player's core to the tile's we just landed on
                tileToMoveTo.GetComponent<RandomColor>().PaintTileRandomColor(); // Change the tile's color

                tileToMoveTo = null; // Reset tileToMoveTo
            }
        }

        lastPlayerPosition = gameObject.transform.position;
    }

    // Function that changes the color of the player's core to what tile they're "standing" on
    void CorePainter()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, transform.up, out hit))
            playerCore.GetComponent<Renderer>().material.color = hit.collider.gameObject.GetComponent<Renderer>().material.GetColor("_Color");
    }
}
