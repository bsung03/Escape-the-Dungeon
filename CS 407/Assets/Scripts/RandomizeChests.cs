using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;

public class RandomizeChests : MonoBehaviour
{

    List<string> ChestList = new List<string>();

    public int chestNum;

    public List<GameObject> chests;

    public GameObject chest1;
    public GameObject chest2;
    public GameObject chest3;

    public GameObject key;
    public GameObject powerup;


    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        //filling the ChsetList array
        chestNum = 3;

        // TODO this should be moved to ChestSpawn script in sprint2
        // to pick a random number for chestNum less than 10
        // chestNum = random.Next(10);
        ChestList.Add("Key");
        for (int i = 0; i < chestNum - 1; i++)
        {
            // to pick a powerup from the powerup array
            /* 
             * int r = random.Next(powerup.size);
             * ChestList.Add(powerup[r]);
             * 
             */
            ChestList.Add("Powerup");
        }

        chests.Add(chest1);
        chests.Add(chest2);
        chests.Add(chest3);

        // shuffling ChestList will make the picking random
        ChestList.Shuffle();

        for (int i = 0; i < chestNum; i++)
        {
            //printing the shuffled list
            Debug.Log(ChestList[i]);
            if (ChestList[i].Equals("Key"))
            {
                chests[i].GetComponent<Chest>().item = key;
            }
            else
            {
                chests[i].GetComponent<Chest>().item = powerup;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

// TODO this should be moved to ChestSpawn script in sprint2
static class Ext
{
    public static void Shuffle<T>(this IList<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}