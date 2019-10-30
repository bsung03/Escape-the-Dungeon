using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomStatus : MonoBehaviour
{
    public GameObject top, bottom, left, right, player;

    public TextMeshPro GoldText, KeyText, ScoreText;

    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject chestPrefab, keyPrefab, powerupPrefab;
    List<GameObject> chests = new List<GameObject>();

    string currRoom;

    public RoomsStats currR = new RoomsStats();

    // Start is called before the first frame update
    void Start()
    {
        top = GameObject.Find("topDoor");
        left = GameObject.Find("leftDoor");
        right = GameObject.Find("rightDoor");
        bottom = GameObject.Find("bottomDoor");

        player = GameObject.FindWithTag("Player");
        currRoom = SceneManager.GetActiveScene().name;

        //TODO: search for the currRoom in the roomstats list 
        
        foreach (RoomsStats r in Menu.roomsStats)
        {
            Debug.Log("comparing with: " + currRoom + "with: " + r.roomName);
            if (r.roomName.Equals(currRoom))
            {
                Debug.Log("if equals worked");
                currR = r;
                Debug.Log("room name:" + currR);
                break;
            }
        }

        updateDoors();

        if (Menu.enterDoorLocation.Equals("bottom"))
        {
            Debug.Log("open");
            top.GetComponent<SpriteRenderer>().enabled = false;
            top.GetComponent<Door>().open_render1.enabled = true;
            top.GetComponent<Door>().open_render2.enabled = true;
            top.GetComponent<Door>().open = true;

            //change player location
            player.transform.position = top.transform.position + new Vector3(0, -3, 0);
        }
        else if (Menu.enterDoorLocation.Equals("top"))
        {
            bottom.GetComponent<SpriteRenderer>().enabled = false;
            bottom.GetComponent<Door>().open_render1.enabled = true;
            bottom.GetComponent<Door>().open_render2.enabled = true;
            bottom.GetComponent<Door>().open = true;

            //change player location
            player.transform.position = bottom.transform.position + new Vector3(0, +3, 0);
        }
        else if (Menu.enterDoorLocation.Equals("left"))
        {
            right.GetComponent<SpriteRenderer>().enabled = false;
            right.GetComponent<Door>().open_render1.enabled = true;
            right.GetComponent<Door>().open_render2.enabled = true;
            right.GetComponent<Door>().open = true;

            //change player location
            player.transform.position = right.transform.position + new Vector3(-3, 0, 0);
        }
        else if (Menu.enterDoorLocation.Equals("right"))
        {
            left.GetComponent<SpriteRenderer>().enabled = false;
            left.GetComponent<Door>().open_render1.enabled = true;
            left.GetComponent<Door>().open_render2.enabled = true;
            left.GetComponent<Door>().open = true;

            //change player location
            player.transform.position = left.transform.position + new Vector3(3, 0, 0);
        }

        if (currR.roomName.Equals("invalid"))
        {
            Debug.Log("not found in list");
            randomizeChests();
        }
        else
        {
            showNonopenedChests();
            Debug.Log("found in list");
            if (currR.isTopDoorOpen)
            {
                Debug.Log("open");
                top.GetComponent<SpriteRenderer>().enabled = false;
                top.GetComponent<Door>().open_render1.enabled = true;
                top.GetComponent<Door>().open_render2.enabled = true;
                top.GetComponent<Door>().open = true;
            }
            else
            {
                Debug.Log("close");
            }
            if (currR.isBottomDoorOpen)
            {
                bottom.GetComponent<SpriteRenderer>().enabled = false;
                bottom.GetComponent<Door>().open_render1.enabled = true;
                bottom.GetComponent<Door>().open_render2.enabled = true;
                bottom.GetComponent<Door>().open = true;
            }
            if (currR.isRightDoorOpen)
            {
                right.GetComponent<SpriteRenderer>().enabled = false;
                right.GetComponent<Door>().open_render1.enabled = true;
                right.GetComponent<Door>().open_render2.enabled = true;
                right.GetComponent<Door>().open = true;
            }
            if (currR.isLeftDoorOpen)
            {
                left.GetComponent<SpriteRenderer>().enabled = false;
                left.GetComponent<Door>().open_render1.enabled = true;
                left.GetComponent<Door>().open_render2.enabled = true;
                left.GetComponent<Door>().open = true;
            }
            //open opened doors
            Menu.roomsStats.Remove(currR);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //updating gold amount
        GoldText.text = player.GetComponent<PlayerController>().gold.ToString();

        //updating keys amount
        KeyText.text = player.GetComponent<PlayerController>().keys.ToString();

        //updating score amount
        ScoreText.text = "Score: " + player.GetComponent<PlayerController>().score.ToString();
    }

    void updateDoors()
    {
        string currRoom = SceneManager.GetActiveScene().name;
        Debug.Log("updating doors of room:" + currRoom);

        int roomNum;
        if (currRoom.Equals("Boss"))
        {
            roomNum = 8;
        }
        else if (currRoom.Equals("Test"))
        {
            roomNum = 9;
        }
        else
        {
            Int32.TryParse(currRoom, out roomNum);
        }

        top.GetComponent<Door>().location = "top";
        bottom.GetComponent<Door>().location = "bottom";
        right.GetComponent<Door>().location = "right";
        left.GetComponent<Door>().location = "left";

        int roomIndex = Menu.Rooms.IndexOf(roomNum);

        if (roomIndex == 0)
        {
            //upper left corner
            left.SetActive(false);
            top.SetActive(false);
            right.GetComponent<Door>().room = Menu.Rooms[1];
            bottom.GetComponent<Door>().room = Menu.Rooms[3];
        }
        else if (roomIndex == 1)
        {
            //upper center
            top.SetActive(false);
            left.GetComponent<Door>().room = Menu.Rooms[0];
            right.GetComponent<Door>().room = Menu.Rooms[2];
            bottom.GetComponent<Door>().room = Menu.Rooms[4];
        }
        else if (roomIndex == 2)
        {
            //upper right corner
            right.SetActive(false);
            top.SetActive(false);
            left.GetComponent<Door>().room = Menu.Rooms[1];
            bottom.GetComponent<Door>().room = Menu.Rooms[5];
        }
        else if (roomIndex == 3)
        {
            //center left
            left.SetActive(false);
            top.GetComponent<Door>().room = Menu.Rooms[0];
            right.GetComponent<Door>().room = Menu.Rooms[4];
            bottom.GetComponent<Door>().room = Menu.Rooms[6];
        }
        else if (roomIndex == 4)
        {
            //center
            top.GetComponent<Door>().room = Menu.Rooms[1];
            bottom.GetComponent<Door>().room = Menu.Rooms[7];
            right.GetComponent<Door>().room = Menu.Rooms[5];
            left.GetComponent<Door>().room = Menu.Rooms[3];
        }
        else if (roomIndex == 5)
        {
            //center right
            right.SetActive(false);
            top.GetComponent<Door>().room = Menu.Rooms[2];
            left.GetComponent<Door>().room = Menu.Rooms[4];
            bottom.GetComponent<Door>().room = Menu.Rooms[8];
        }
        else if (roomIndex == 6)
        {
            //lower left corner
            left.SetActive(false);
            bottom.SetActive(false);
            top.GetComponent<Door>().room = Menu.Rooms[3];
            right.GetComponent<Door>().room = Menu.Rooms[7];
        }
        else if (roomIndex == 7)
        {
            //lower center
            bottom.SetActive(false);
            top.GetComponent<Door>().room = Menu.Rooms[4];
            left.GetComponent<Door>().room = Menu.Rooms[6];
            right.GetComponent<Door>().room = Menu.Rooms[8];
        }
        else if (roomIndex == 8)
        {
            //lower right corner
            bottom.SetActive(false);
            right.SetActive(false);
            top.GetComponent<Door>().room = Menu.Rooms[5];
            left.GetComponent<Door>().room = Menu.Rooms[7];
        }
    }

    void randomizeChests()
    {
        // randomizing the number of chests between 3 and 10 for the center room
        System.Random random = new System.Random();
        int chestNum = random.Next(3, 11);

        for (int i = 0; i < chestNum; i++)
        {
            // Instantiate at position (0, 0, 0) and zero rotation.
            chests.Add(Instantiate(chestPrefab, new Vector3(10, 10, -1), Quaternion.identity));
        }

        // shuffling ChestList will make the picking at random
        chests.Shuffle();

        chests[0].GetComponent<Chest>().item = keyPrefab;

        for (int i = 1; i < chestNum - 1; i++)
        {
            // to pick a powerup from the powerup array
            /*            
             * int r = random.Next(powerup.size);
             * chests[i].GetComponent<Chest>().item = powerup[r];
             * 
             */
            chests[i].GetComponent<Chest>().item = powerupPrefab;
        }

    }

    void showNonopenedChests()
    {
        // randomizing the number of chests between 3 and 10 for the center room
        System.Random random = new System.Random();
        int chestNum = random.Next(3, 11);

        for (int i = 0; i < currR.unopenedChests.Count; i++)
        {
            // Instantiate at position (0, 0, 0) and zero rotation.
            chests.Add(Instantiate(chestPrefab, currR.unopenedChests[i].chestLocation, Quaternion.identity));
            chests[i].GetComponent<Chest>().item = currR.unopenedChests[i].chestItem;

            // to pick a powerup from the powerup array
            /*            
             * int r = random.Next(powerup.size);
             * chests[i].GetComponent<Chest>().item = powerup[r];
             * 
             */
        }
    }
}