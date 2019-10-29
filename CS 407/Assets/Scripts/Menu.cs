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
    public AudioMixer audioMixer;
    public GameObject player;
    public TextMeshProUGUI gold_text;
    public TextMeshProUGUI score_text;

    public TextMeshProUGUI timerText;
    private float startTime;

    public static List<int> Rooms = new List<int>();

    void Start()
    {
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

    public void StartGame()
    {
        // before starting the game, randomize the order of the rooms grid
        randomizeRooms();
        GameIsPaused = false;
        // center room is the one stored at index 4
        SceneManager.LoadScene(Rooms[4]);
    }

    public void randomizeRooms()
    {
        // Add all room numbers and shuffle the list
        for (int i = 1; i < 8; i++)
        {
            Rooms.Add(i);
        }
        Rooms.Shuffle();
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

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
