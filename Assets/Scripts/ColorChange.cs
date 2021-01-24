using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("NextColor", 0, 1); // Call NextColor() every 1 sec
    }

    void NextColor()
    {
        //Change the color to a random one
        gameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
