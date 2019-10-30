using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer open_render1, open_render2;
    public bool open = false;
    public int room;
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

        if (dist <= 3 && open)
        {
            // Save room status
            /*bool isTopDoorOpen = false;
            bool isBottomDoorOpen = false;
            bool isRightDoorOpen = false;
            bool isLeftDoorOpen = false;

            if (map.GetComponent<RoomStatus>().top.activeSelf)
            {
                isTopDoorOpen = map.GetComponent<RoomStatus>().top.GetComponent<Door>().open;
            }
            if (map.GetComponent<RoomStatus>().bottom.activeSelf)
            {
                isBottomDoorOpen = map.GetComponent<RoomStatus>().bottom.GetComponent<Door>().open;
            }
            if (map.GetComponent<RoomStatus>().right.activeSelf)
            {
                isRightDoorOpen = map.GetComponent<RoomStatus>().right.GetComponent<Door>().open;
            }
            if (map.GetComponent<RoomStatus>().left.activeSelf)
            {
                isLeftDoorOpen = map.GetComponent<RoomStatus>().left.GetComponent<Door>().open;
            }
            RoomsStats currRoom = new RoomsStats(SceneManager.GetActiveScene().name, isTopDoorOpen, isBottomDoorOpen, isRightDoorOpen, isLeftDoorOpen);*/
            RoomsStats currRoom = new RoomsStats(SceneManager.GetActiveScene().name,
                map.GetComponent<RoomStatus>().top.GetComponent<Door>().open,
                map.GetComponent<RoomStatus>().bottom.GetComponent<Door>().open,
                map.GetComponent<RoomStatus>().right.GetComponent<Door>().open,
                map.GetComponent<RoomStatus>().left.GetComponent<Door>().open);
            Debug.Log("adding: " + SceneManager.GetActiveScene().name);
            Menu.roomsStats.Add(currRoom);
            
            // Load next room
            SceneManager.LoadScene(room);
        }
    }


}
