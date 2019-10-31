using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
public class Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject characterMenu;
    public GameObject globalData;
    public AudioMixer audioMixer;
    public GameObject player;
    public GameObject melee;
    public GameObject gunner;
    public GameObject miniscene;
    public TextMeshProUGUI gold_text;
    public TextMeshProUGUI score_text;

    public TextMeshProUGUI timerText;
    private float startTime;
    public static List<int> Rooms = new List<int>();
    public static int currRoomID;

    public static int roomToLoad;

    void Start()
    {
        //select melee player
        player = GameObject.Find("Player(Clone)");

        //player not melee, select gunner player
        if (player == null) {
            player = GameObject.Find("Player 1(Clone)");
        }
        startTime = Time.time;
    }

    void Update()
    {
        if (!GameIsPaused)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            timerText.SetText(minutes + ":" + seconds);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
     
                    Pause();
   
            }
        }
        else
        {
            gold_text.SetText("Gold: " + player.GetComponent<PlayerController>().gold.ToString());
            score_text.SetText("Score: " + player.GetComponent<PlayerController>().score.ToString());
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void showCharacterSelect() {
        pauseMenuUI.SetActive(false);
        characterMenu.SetActive(true);
    }
    public void StartGame()
    {
        // before starting the game, randomize the order of the rooms grid
        randomizeRooms();
        GameIsPaused = false;
        // center room is the one stored at index 4
        currRoomID = Rooms[4];
        roomToLoad = Rooms[0];
        SceneManager.LoadScene(roomToLoad, LoadSceneMode.Additive);
        /*for (int i = 1; i < 9 && i != 4; i++)
        {
            //load rooms
            SceneManager.LoadScene(Rooms[i], LoadSceneMode.Additive);
            miniscene = GameObject.Find("miniScene" + Rooms[i]);
            miniscene.SetActive(false);
        }*/
    }

    public void randomizeRooms()
    {
        // Add all room numbers and shuffle the list
        for (int i = 1; i < 10; i++)
        {
            Rooms.Add(i);
        }
        Rooms.Shuffle();

        // The following printing is just for Testing
        Debug.Log("shuffled list:");
        foreach (int k in Rooms)
        {
            Debug.Log(k);
        }

        System.Random random = new System.Random();
        int r = random.Next(1, 8); // creates a number between 1 and 7 for the center room
        Debug.Log("random number: " + r);
        Debug.Log("random index: " + Rooms.IndexOf(r));

        Rooms[Rooms.IndexOf(r)] = Rooms[4];
        Rooms[4] = r;

        // The following printing is just for Testing
        Debug.Log("changing center room list:");
        foreach (int k in Rooms)
        {
            Debug.Log(k);
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void returnToMainMenu() {
        Destroy(player);
        SceneManager.LoadScene("Menu");
    }
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void meleeClick() {
        player = Instantiate(melee, new Vector3(0,0,0), Quaternion.identity, null);
        StartGame();
    }
    public void gunnerClick() {
        player = Instantiate(gunner, new Vector3(0, 0, 0), Quaternion.identity, null);
        StartGame();
    }
}
