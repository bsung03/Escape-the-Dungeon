using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System;
using System.IO;
using System.Text;
using TMPro;
public class SubmitScore : MonoBehaviour
{
    public int finalScore = 0;
    public InputField username;
    public TextMeshProUGUI score;
    public Text errorMessage;
    public GameObject submitButton;
    private string uname;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void submit(){
        if(!string.IsNullOrWhiteSpace(username.text)){
            // trim white spaces from start and end of username
            uname = ((username.text).TrimStart()).TrimEnd();
            string scores = score.GetComponent<TextMeshProUGUI>().text;

            string req = String.Format("https://urlshortenerfcc.glitch.me/newScore/{0}&&{1}", uname, scores.Substring(7));

            Debug.Log(req);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(req);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            submitButton.SetActive(false);
        }
    }
}
