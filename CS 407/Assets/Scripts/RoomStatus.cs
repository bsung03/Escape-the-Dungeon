using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomStatus : MonoBehaviour
{
    GameObject top, bottom, left, right;

    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject chestPrefab, keyPrefab, powerupPrefab;
    List<GameObject> chests = new List<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        updateDoors();
        randomizeChests();
    }

    void updateDoors()
    {
        string roomName = SceneManager.GetActiveScene().name;
        Debug.Log(roomName);

        int roomNum;
        if (roomName.Equals("Boss"))
        {
            roomNum = 8;
        }
        else if (roomName.Equals("Test"))
        {
            roomNum = 9;
        }
        else
        {
            Int32.TryParse(roomName, out roomNum);
        }

        top = GameObject.Find("topDoor");
        left = GameObject.Find("leftDoor");
        right = GameObject.Find("rightDoor");
        bottom = GameObject.Find("bottomDoor");

        int roomIndex = Menu.Rooms.IndexOf(roomNum);

        if (roomIndex == 0)
        {
            //upper left corner
            Destroy(left);
            Destroy(top);
            right.GetComponent<Door>().room = Menu.Rooms[1];
            bottom.GetComponent<Door>().room = Menu.Rooms[3];
        }
        else if (roomIndex == 1)
        {
            //upper center
            Destroy(top);
            left.GetComponent<Door>().room = Menu.Rooms[0];
            right.GetComponent<Door>().room = Menu.Rooms[2];
            bottom.GetComponent<Door>().room = Menu.Rooms[4];
        }
        else if (roomIndex == 2)
        {
            //upper right corner
            Destroy(right);
            Destroy(top);
            left.GetComponent<Door>().room = Menu.Rooms[1];
            bottom.GetComponent<Door>().room = Menu.Rooms[5];
        }
        else if (roomIndex == 3)
        {
            //center left
            Destroy(left);
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
            Destroy(right);
            top.GetComponent<Door>().room = Menu.Rooms[2];
            left.GetComponent<Door>().room = Menu.Rooms[4];
            bottom.GetComponent<Door>().room = Menu.Rooms[8];
        }
        else if (roomIndex == 6)
        {
            //lower left corner
            Destroy(left);
            Destroy(bottom);
            top.GetComponent<Door>().room = Menu.Rooms[3];
            right.GetComponent<Door>().room = Menu.Rooms[7];
        }
        else if (roomIndex == 7)
        {
            //lower center
            Destroy(bottom);
            top.GetComponent<Door>().room = Menu.Rooms[4];
            left.GetComponent<Door>().room = Menu.Rooms[6];
            right.GetComponent<Door>().room = Menu.Rooms[8];
        }
        else if (roomIndex == 8)
        {
            //lower right corner
            Destroy(bottom);
            Destroy(right);
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

    // Update is called once per frame
    void Update()
    {

    }
}