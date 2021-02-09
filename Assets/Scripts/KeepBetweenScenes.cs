using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepBetweenScenes : MonoBehaviour
{
    static KeepBetweenScenes instance = null;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // If an instance of this object exists, don't create a second one
        if (instance!= null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
