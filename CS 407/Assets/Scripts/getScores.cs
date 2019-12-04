using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;
//using Assets ayyyyyy fam;
public class getScores : MonoBehaviour
{

    public GameObject playerEntryPrefab;
    public int offset = 0;


    struct person
    {
        public int rank;
        public string name;
        public string score;
    }
    //array that holds all score entries
    person[] people = new person[10];

    //person[] people = new person[1000];
    // Start is called before the first frame update
    void Start()
    {
        getSetScores();
    }


    public void parseScores(string jsonResponse) {
        var x = jsonResponse.Split('\"');
        string[] scores = new string[20];
        int index = 0;
        for (int i = 0; i < x.Length; i++) {
            if (x[i].Contains("{") || x[i].Contains("}") || x[i].Contains("[") || x[i].Contains("]") || x[i].Contains(",") || x[i].Contains(":") || x[i].Contains("name") || x[i].Contains("score"))
            {

            }
            else {
                scores[index++] = x[i];
            }

        }
        index = 0;
        for (int i = 0; i < scores.Length; i+=2) {
            people[index].name = scores[i];
            people[index].score = scores[i + 1];
            people[index++].rank = offset + index;
        }

    }
    /// <summary>
    /// A debugging function that prints each person in the people array and their scores
    /// </summary>
    /// 

    public void printPeople()
    {
        for (int i = 0; i < people.Length; i++)
        {
            Debug.Log("Person: " + people[i].name + " Score: " + people[i].score);
        }
    }


    public void setScores() {
        for (int i = 0; i < 10; i++) {
            GameObject name = GameObject.Find((i+1).ToString() + "number/name");
            GameObject score = GameObject.Find((i+1).ToString() + "score");
            // Debug.Log(people[i].name);
            name.GetComponent<Text>().text = people[i].rank+". "+people[i].name;
            score.GetComponent<Text>().text = "Score: " + people[i].score;
        }
    }

    public void nextClick() {
        offset += 10;
        getSetScores();
        Debug.Log("next click");
    }

    public void backClick() {
        if (offset > 0) {
            offset -= 10;
        }
        getSetScores();
        Debug.Log("back click");
    }

    public void getSetScores() {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://urlshortenerfcc.glitch.me/getScores/{0}", offset.ToString()));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        if (jsonResponse != "[]")
        {
            parseScores(jsonResponse);
            setScores();
        }
        else {
            offset -= 10;
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("menu");
    }



}
