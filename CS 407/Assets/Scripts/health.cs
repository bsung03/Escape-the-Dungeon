using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the player
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        //receive the player's health from their stats array
        double playerHealth = (double)player.stats[0];

        //receive the player's max health from their stats array
        double playerMaxHealth = (double)player.stats[1];

        //Calculate the percent and multiply it by the initial width of the bar (e.g. at 100% it will equal 223.813)
        double percent = playerHealth / playerMaxHealth * 223.813;
        RectTransform rt = this.GetComponent<RectTransform>();

        //set new size of the health bar
        rt.sizeDelta = new Vector2((float)percent, (float)33.152);
    }
}
