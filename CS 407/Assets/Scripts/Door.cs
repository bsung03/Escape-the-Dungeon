using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Door : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer open_render1, open_render2;
    bool open = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (Input.GetKeyDown(KeyCode.P) && dist <= 2 && player.GetComponent<PlayerController>().keys >= 1 && !open)
        {
            player.GetComponent<PlayerController>().keys--;
            print("Opened Door");
            this.GetComponent<SpriteRenderer>().enabled = false;
            open_render1.enabled = true;
            open_render2.enabled = true;
            open = true;
        }
    }


}
