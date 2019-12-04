using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialNavigation : MonoBehaviour
{
    public int page = 0;
    public GameObject[] pages;
    public GameObject curr, newcurr, nextbtn, prevbtn;
    // Start is called before the first frame update
    void Start()
    {
        page = 0;
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
            prevbtn.SetActive(true);
            if(page == 4)
            {
                nextbtn.SetActive(false);
            }
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
            nextbtn.SetActive(true);
            if (page == 0)
            {
                prevbtn.SetActive(false);
            }
        }
    }
}
