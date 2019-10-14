using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBoss : MonoBehaviour
{
    public GameObject[] Bosses;
    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, Bosses.Length);
        Bosses[r].SetActive(true)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
