using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class statBar : MonoBehaviour
{
    public TextMeshPro GoldText, KeyText, ScoreText;
    public GameObject player;

    GameObject[] potentialPlayers;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if(player == null)
        {
            player = GameObject.Find("Player(Clone)");
            if (player == null)
            {
                player = GameObject.Find("Player 1(Clone)");
            }
        }

        if (player == null)
        {
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
        GoldText.text = player.GetComponent<PlayerController>().gold.ToString();

        //updating gold amount
        KeyText.text = player.GetComponent<PlayerController>().keys.ToString();
  
        ScoreText.text = "Score: " + player.GetComponent<PlayerController>().score.ToString();
    }
}
