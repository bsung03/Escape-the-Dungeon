using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Text;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("WAAAAAAAAAAAAAAAAAA" + Menu.GetComponent<Menu>().StageNum);
        Text.GetComponent<Text>().text = "1";
    }
}
