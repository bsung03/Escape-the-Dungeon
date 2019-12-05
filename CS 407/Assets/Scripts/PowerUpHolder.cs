using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
public class PowerUpHolder : MonoBehaviour
{
    int index;
    GameObject player;
    GameObject[] potentialPlayers;
    public GameObject tutorial;
    string[] tutrial_strings = { "Decrease Enemy Speed 5 sec.", "Increase Move Speed", "Increase Health", "Increase Attack", "Change Attack Type" };
    // Start is called before the first frame update
    void Start()
    {
        index = Random.Range(0, 5);
        if (player == null)
        {
            potentialPlayers = GameObject.FindGameObjectsWithTag("Player");
            print("Potential Length: " + potentialPlayers.Length.ToString());
            if (potentialPlayers.Length > 0)
            {
                player = potentialPlayers[0];
            }
            else
            {
                potentialPlayers = GameObject.FindGameObjectsWithTag("roll");
                if (potentialPlayers.Length > 0)
                {
                    player = potentialPlayers[0];
                }
                else
                {
                    return;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            potentialPlayers = GameObject.FindGameObjectsWithTag("Player");
            print("Potential Length: " + potentialPlayers.Length.ToString());
            if (potentialPlayers.Length > 0)
            {
                player = potentialPlayers[0];
            }
            else
            {
                potentialPlayers = GameObject.FindGameObjectsWithTag("roll");
                if (potentialPlayers.Length > 0)
                {
                    player = potentialPlayers[0];
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist <= 1.5f)
            {
                player.SendMessage("IncreasePowerUp", index);
                GameObject item_clone = Instantiate(tutorial, this.transform.position, Quaternion.identity);
                item_clone.GetComponent<TextMeshPro>().SetText(tutrial_strings[index]);
                Destroy(item_clone, 5.0f);
                //SceneManager.GetSceneByBuildIndex(Menu.currRoomID).GetRootGameObjects()[0].transform);
                Destroy(this.gameObject);
            }
        }




    }
}
