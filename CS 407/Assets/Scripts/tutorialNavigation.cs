using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialNavigation : MonoBehaviour
{
    public int page = 0;
    public GameObject page1, page2, page3, page4, page5;
    public GameObject[] pages;
    public GameObject curr, newcurr;
    // Start is called before the first frame update
    void Start()
    {
        page = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void next()
    {
        Debug.Log("next pressed");
        if (page < 4)
        {
            Debug.Log("changing pages");
            curr = pages[page];
            page++;
            newcurr = pages[page];
            curr.SetActive(false);
            newcurr.SetActive(true);
        }
    }
    public void prev()
    {
        Debug.Log("prev pressed");
        if (page > 0)
        {
            Debug.Log("changing pages");
            curr = pages[page];
            page--;
            newcurr = pages[page];
            curr.SetActive(false);
            newcurr.SetActive(true);
        }
    }
}
