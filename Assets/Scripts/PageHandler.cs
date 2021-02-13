using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageHandler : MonoBehaviour
{
    private Transform pages;

    private int activePageNumber;
    private int numberOfPages;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        pages = gameObject.transform.Find("Pages");

        numberOfPages = pages.childCount;
    }

    // OnEnable is called when the object becomes enabled and active (after Awake)
    void OnEnable() 
    {
        activePageNumber = 1;

        // Set all pages to inactive
        foreach (Transform page in pages)
        {
            page.gameObject.SetActive(false);
        }

        // Set first page to active
        pages.Find("Page " + activePageNumber).gameObject.SetActive(true);
    }

    private void ChangePage(int pageNumber)
    {
        if (pageNumber >= 1 && pageNumber <= numberOfPages) // Check if pageNumber is within range [1, numberOfPages]
        {
            pages.Find("Page " + activePageNumber).gameObject.SetActive(false);
            pages.Find("Page " + pageNumber).gameObject.SetActive(true);
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
