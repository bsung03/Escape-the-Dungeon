using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Chest : MonoBehaviour
{
    public GameObject player;
    public TextMeshPro textMeshPro;
    public SpriteRenderer open_render;
    public GameObject item;
    public int cost = 10;
    bool open = false;
    GameObject[] potential_players;
    // Start is called before the first frame update
    void Start()
    {
        potential_players = GameObject.FindGameObjectsWithTag("Player");
        if(potential_players.Length > 0)
        {
            player = potential_players[0];
            print("Player Set!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (potential_players.Length == 0)
        {
            potential_players = GameObject.FindGameObjectsWithTag("Player");
            if (potential_players.Length > 0)
            {
                player = potential_players[0];
                print("Player Set!");
            }
        }
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist <= 1.75)
        {
            textMeshPro.enabled = true;
        }
        else
        {
            textMeshPro.enabled = false;
        }
        if(Input.GetKeyDown(KeyCode.E) && dist <=1.75 && player.GetComponent<PlayerController>().gold >= cost && !open)
        {
            player.GetComponent<PlayerController>().gold -= cost;
            print("Opened Chest");
            this.GetComponent<SpriteRenderer>().enabled = false;
            open_render.enabled = true;
            open = true;
            GameObject item_clone = Instantiate(item, transform.position - transform.up, Quaternion.identity, null);
            item_clone.SetActive(true);
            Destroy(this.gameObject, 2);
        }
    }

  
}
