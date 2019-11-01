using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayswordSwingSound : MonoBehaviour
{
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            AudioSource.Play();
        }

        

    }
}
