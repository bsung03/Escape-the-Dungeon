using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomStatus : MonoBehaviour
{
    GameObject top, bottom, left, right, player, miniscene;

    public TextMeshPro GoldText, KeyText, ScoreText;

    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject chestPrefab, keyPrefab, powerupPrefab;
    List<GameObject> chests = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        updateDoors();
        randomizeChests();

        int i = Menu.Rooms.IndexOf(Menu.roomToLoad);
        if(i == 4)
        {
            Destroy(GameObject.Find("miniSceneMenu"));
        }
        if (i != 4)
        {
            miniscene = GameObject.Find("miniScene" + Menu.roomToLoad);
            miniscene.SetActive(false);
        }

        
        if ((i+1) < 9)
        {
            Menu.roomToLoad = Menu.Rooms[i+1];
            SceneManager.LoadScene(Menu.roomToLoad, LoadSceneMode.Additive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateDoors()
    {
        /*string roomName = SceneManager.GetActiveScene().name;
        Debug.Log(roomName);

        int roomNum;
        if (roomName.Equals("Boss"))
        {
            roomNum = 8;
        }
        else
        {
            Int32.TryParse(roomName, out roomNum);
        }*/

        top = GameObject.Find("topDoor" + Menu.roomToLoad);
        left = GameObject.Find("leftDoor" + Menu.roomToLoad);
        right = GameObject.Find("rightDoor" + Menu.roomToLoad);
        bottom = GameObject.Find("bottomDoor" + Menu.roomToLoad);

        int roomIndex = Menu.Rooms.IndexOf(Menu.roomToLoad);

        top.GetComponent<Door>().location = "top";
        bottom.GetComponent<Door>().location = "bottom";
        right.GetComponent<Door>().location = "right";
        left.GetComponent<Door>().location = "left";

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
            int x = random.Next(-22, 22);
            int y = random.Next(-12, 12);
            // Instantiate at position (0, 0, 0) and zero rotation.
            chests.Add(Instantiate(chestPrefab, new Vector3(x, y, -1), Quaternion.identity, SceneManager.GetSceneByBuildIndex(Menu.roomToLoad).GetRootGameObjects()[0].transform));
        }

        // shuffling ChestList will make the picking at random
        chests.Shuffle();

        chests[0].GetComponent<Chest>().item = keyPrefab;

        for (int i = 1; i < chestNum; i++)
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
}