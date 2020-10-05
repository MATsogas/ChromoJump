using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private GameObject playerCore;

    // Start is called before the first frame update
    void Start()
    {
        playerCore = gameObject.transform.Find("Player Core").gameObject; // Find player's core (child GameObject named PlayerCore)
        
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, transform.up, out hit))
            playerCore.GetComponent<Renderer>().material.color = hit.collider.gameObject.GetComponent<Renderer>().material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
