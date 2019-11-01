using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject player, miniscene, top, bottom, left, right;
    public SpriteRenderer open_render1, open_render2;
    bool open = false;
    public int room;

    public Scene nextRoom;

    public string location;

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
            nextRoom = SceneManager.GetSceneByBuildIndex(room);
            miniscene = nextRoom.GetRootGameObjects()[0];
            player.GetComponent<PlayerController>().keys--;
            print("Opened Door");
            this.GetComponent<SpriteRenderer>().enabled = false;
            open_render1.enabled = true;
            open_render2.enabled = true;
            open = true;

            //SceneManager.LoadScene("Test");
            if (location.Equals("bottom"))
            {
                Debug.Log("open");
                top = miniscene.transform.Find("Map").Find("topDoor" + room).gameObject;
                top.GetComponent<SpriteRenderer>().enabled = false;
                top.GetComponent<Door>().open_render1.enabled = true;
                top.GetComponent<Door>().open_render2.enabled = true;
                top.GetComponent<Door>().open = true;
            }
            else if (location.Equals("top"))
            {
                bottom = miniscene.transform.Find("Map").Find("bottomDoor" + room).gameObject;
                bottom.GetComponent<SpriteRenderer>().enabled = false;
                bottom.GetComponent<Door>().open_render1.enabled = true;
                bottom.GetComponent<Door>().open_render2.enabled = true;
                bottom.GetComponent<Door>().open = true;
            }
            else if (location.Equals("left"))
            {
                right = miniscene.transform.Find("Map").Find("rightDoor" + room).gameObject;
                right.GetComponent<SpriteRenderer>().enabled = false;
                right.GetComponent<Door>().open_render1.enabled = true;
                right.GetComponent<Door>().open_render2.enabled = true;
                right.GetComponent<Door>().open = true;
            }
            else if (location.Equals("right"))
            {
                left = miniscene.transform.Find("Map").Find("leftDoor" + room).gameObject;
                left.GetComponent<SpriteRenderer>().enabled = false;
                left.GetComponent<Door>().open_render1.enabled = true;
                left.GetComponent<Door>().open_render2.enabled = true;
                left.GetComponent<Door>().open = true;
            }
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
