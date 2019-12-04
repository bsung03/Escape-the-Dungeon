using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PowerUpText : MonoBehaviour
{

    TextMeshPro text;
    GameObject player;
    GameObject[] potentialPlayers;
    public int powerup_index;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<TextMeshPro>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            text.SetText("");
            potentialPlayers = GameObject.FindGameObjectsWithTag("Player");
            print("Potential Length: " + potentialPlayers.Length.ToString());
            if (potentialPlayers.Length > 0)
            {
                player = potentialPlayers[0];
            }
            else
            {
                potentialPlayers = GameObject.FindGameObjectsWithTag("roll");
                if (potentialPlayers.Length > 0)
                {
                    player = potentialPlayers[0];
                }
            }
            return;
        }
        else
        {
            text.SetText(player.GetComponent<PlayerController>().powerups[powerup_index].ToString());
        }


    }
}
