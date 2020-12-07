using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitToBeEnabled : MonoBehaviour
{
    public float timeDisabledOnStartInSeconds = 1f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(timeDisabledOnStartInSeconds);
        gameObject.GetComponent<Button>().interactable = true;
    }
}
