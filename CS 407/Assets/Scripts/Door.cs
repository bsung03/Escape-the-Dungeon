using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject player;
    public GameObject[] nonopenedChests;
    public SpriteRenderer open_render1, open_render2;
    public bool open = false;
    public int room;
    public string location;
    GameObject map;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        map = GameObject.Find("Map");
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

        if (dist <= 2 && open)
        {
            nonopenedChests = GameObject.FindGameObjectsWithTag("Chest");
            ChestStruct currChest;
            RoomsStats currRoom = new RoomsStats(SceneManager.GetActiveScene().name,
                map.GetComponent<RoomStatus>().top.GetComponent<Door>().open,
                map.GetComponent<RoomStatus>().bottom.GetComponent<Door>().open,
                map.GetComponent<RoomStatus>().right.GetComponent<Door>().open,
                map.GetComponent<RoomStatus>().left.GetComponent<Door>().open);
            Debug.Log("adding: " + SceneManager.GetActiveScene().name);
            foreach (GameObject c in nonopenedChests)
            {
                currChest.chestLocation = c.transform.position;
                currChest.chestItem = c.GetComponent<Chest>().item;
                currRoom.unopenedChests.Add(currChest);
            }

            Menu.roomsStats.Add(currRoom);

            Menu.enterDoorLocation = location;
            
            // Load next room
            SceneManager.LoadScene(room);
        }
    }


}
