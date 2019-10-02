using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public int sampleHealth;
    public int sampleMaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        sampleHealth = 100;
        sampleMaxHealth = 100;

    }

    // Update is called once per frame
    void Update()
    {
        //receive the player's health from their stats array
        double enemyHealth = (double)sampleHealth;

        //receive the player's max health from their stats array
        double enemyMaxHealth = (double)sampleMaxHealth;

        //Calculate the percent and multiply it by the initial width of the bar (e.g. at 100% it will equal 223.813)
        double percent = enemyHealth/ enemyMaxHealth * 0.0857904;

        //set new size of the health bar
        transform.localScale = new Vector2((float)percent, (float)0.09419625);
    }
}
