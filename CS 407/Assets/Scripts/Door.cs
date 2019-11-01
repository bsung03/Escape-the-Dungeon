using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject player, miniscene;
    public SpriteRenderer open_render1, open_render2;
    bool open = false;
    public int room;

    public Scene nextRoom;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player(Clone)");
        if(player == null)
        {
            player = GameObject.Find("Player 1(Clone)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (Input.GetKeyDown(KeyCode.P) && dist <= 3 && player.GetComponent<PlayerController>().keys >= 1 && !open)
        {
            player.GetComponent<PlayerController>().keys--;
            print("Opened Door");
            this.GetComponent<SpriteRenderer>().enabled = false;
            open_render1.enabled = true;
            open_render2.enabled = true;
            open = true;

            //SceneManager.LoadScene("Test");

        }

        if (dist <= 3 && open)
        {
            nextRoom = SceneManager.GetSceneByBuildIndex(room);
            miniscene = nextRoom.GetRootGameObjects()[0];
            miniscene.SetActive(true);
            miniscene = GameObject.Find("miniScene" + Menu.currRoomID);
            miniscene.SetActive(false);
            
            Menu.currRoomID = room;
        }
    }


}
