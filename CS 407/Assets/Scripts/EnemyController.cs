using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int health = 3;
    public int maxHealth = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0){
            Destroy(gameObject);
            GameObject.Find("Player").GetComponent<PlayerController>().IncreaseScore(1);
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
        Debug.Log("hurt me");
    }
}
