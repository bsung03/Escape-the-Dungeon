using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject player, miniscene, door;
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

        if (Input.GetKeyDown(KeyCode.Q) && dist <= 3 && player.GetComponent<PlayerController>().keys >= 1 && !open)
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
                door = miniscene.transform.Find("Map").Find("topDoor" + room).gameObject;
            }
            else if (location.Equals("top"))
            {
                door = miniscene.transform.Find("Map").Find("bottomDoor" + room).gameObject;
            }
            else if (location.Equals("left"))
            {
                door = miniscene.transform.Find("Map").Find("rightDoor" + room).gameObject;
            }
            else if (location.Equals("right"))
            {
                door = miniscene.transform.Find("Map").Find("leftDoor" + room).gameObject;
            }
            door.GetComponent<SpriteRenderer>().enabled = false;
            door.GetComponent<Door>().open_render1.enabled = true;
            door.GetComponent<Door>().open_render2.enabled = true;
            door.GetComponent<Door>().open = true;
        }

        if (dist <= 3 && open)
        {
            nextRoom = SceneManager.GetSceneByBuildIndex(room);            
            miniscene = nextRoom.GetRootGameObjects()[0];
            miniscene.SetActive(true);

            if (location.Equals("bottom"))
            {
                door = miniscene.transform.Find("Map").Find("topDoor" + room).gameObject;
                player.transform.position = door.transform.position + new Vector3(0, -4, 0);
            }
            else if (location.Equals("top"))
            {
                door = miniscene.transform.Find("Map").Find("bottomDoor" + room).gameObject;
                player.transform.position = door.transform.position + new Vector3(0, 4, 0);
            }
            else if (location.Equals("left"))
            {
                door = miniscene.transform.Find("Map").Find("rightDoor" + room).gameObject;
                player.transform.position = door.transform.position + new Vector3(-4, 0, 0);
            }
            else if (location.Equals("right"))
            {
                door = miniscene.transform.Find("Map").Find("leftDoor" + room).gameObject;
                player.transform.position = door.transform.position + new Vector3(4, 0, 0);
            }

            miniscene = GameObject.Find("miniScene" + Menu.currRoomID);
            miniscene.SetActive(false);

            
            Menu.currRoomID = room;
        }
    }


}
