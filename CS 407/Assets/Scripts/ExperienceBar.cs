using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();

        //receive the player's health from their stats array
        double playerExperience = (double)player.experience;

        //receive the player's max health from their stats array
        double playerNextLevelThreshold = (double)player.expThreshold;

        //Calculate the percent and multiply it by the initial width of the bar (e.g. at 100% it will equal 223.813)
        double percent = playerExperience / playerNextLevelThreshold * 236.6;
        RectTransform rt = this.GetComponent<RectTransform>();

        //set new size of the health bar
        rt.sizeDelta = new Vector2((float)percent, (float)9.11);
    }
}
