using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float playerSpeed;
    
    private GameObject playerCore;

    private Queue<GameObject> movementQueue = new Queue<GameObject>(); // In case the user clicks on another tile before the player lands
    private GameObject tileToMoveTo;

    // Start is called before the first frame update
    void Start()
    {
        playerCore = gameObject.transform.Find("Player Core").gameObject; // Find player's core (child GameObject named "Player Core")
        CorePainter();
    }

    // Update is called once per frame
    void Update()
    {
        // Add on a queue the tiles that are clicked
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) 
            {             
                // Debug.Log("Clicked on: " + hit.collider.gameObject.name + ". (Color: " + hit.collider.gameObject.GetComponent<Renderer>().material.GetColor("_Color") + "), Position: " + hit.collider.gameObject.transform.position + ")");
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
                playerCore.GetComponent<Renderer>().material.color = tileToMoveTo.GetComponent<Renderer>().material.GetColor("_Color");
                //gameObject.transform.parent.gameObject.GetComponent<GameMaster>().ChangeTileColor(tileToMoveTo); // Change the color of the tile player landed on
                tileToMoveTo = null; // Reset tileToMoveTo
            }
        }
    }

    // Function that changes the color of the player's core to what tile they're "standing" on
    void CorePainter()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, transform.up, out hit))
            playerCore.GetComponent<Renderer>().material.color = hit.collider.gameObject.GetComponent<Renderer>().material.GetColor("_Color");
    }
}
