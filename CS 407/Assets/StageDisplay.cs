using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Menu;
    public TextMeshPro xyz;
    void Start()
    {
        //text.SetText("Hello");
    }

    // Update is called once per frame
    void Update()
    {
        xyz.text = "Stages Completed: "; //+ Menu.GetComponent<Menu>().StageNum.ToString();
    }
}
