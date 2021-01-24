using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageHandler : MonoBehaviour
{
    private int activePageNumber;
    private int numberOfPages;

    // Start is called before the first frame update
    void Start()
    {
        activePageNumber = 1;
        numberOfPages = gameObject.transform.childCount;

        // Set all pages to inactive
        foreach (Transform page in gameObject.transform)
        {
            page.gameObject.SetActive(false);
        }

        // Set first page to active
        gameObject.transform.Find("Page " + activePageNumber).gameObject.SetActive(true);
    }

    private void ChangePage(int pageNumber)
    {
        if (pageNumber >= 1 && pageNumber <= numberOfPages) // Check if pageNumber is within range [1, numberOfPages]
        {
            gameObject.transform.Find("Page " + activePageNumber).gameObject.SetActive(false);
            gameObject.transform.Find("Page " + pageNumber).gameObject.SetActive(true);
            activePageNumber = pageNumber;
        } else 
        {
            Debug.LogWarning("Page number outside of range [1, " + numberOfPages + "]");
        }
    }

    /// <summary>
    /// Returns false if the next page is the last one; returns true otherwise.
    /// </summary>
    public bool NextPage()
    {
        ChangePage(activePageNumber + 1);
        return (activePageNumber != numberOfPages);
    }

    /// <summary>
    /// Returns false if the previous page is the first one; returns true otherwise.
    /// </summary>
    public bool PreviousPage()
    {
        ChangePage(activePageNumber - 1);
        return (activePageNumber != 1);
    }
}
