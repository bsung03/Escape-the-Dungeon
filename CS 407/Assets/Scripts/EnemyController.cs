using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int health = 3;
    public int maxHealth = 3;
    public GameObject gold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0){
            Destroy(gameObject);
            //Make a gold ovject where enemy dies
            Instantiate(gold, transform.position, Quaternion.identity, null);

            //Increase player's score
            GameObject.Find("Player").GetComponent<PlayerController>().IncreaseScore(1);

            //Grant player experience
            GameObject.Find("Player").GetComponent<PlayerController>().addExperience(15);
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
        Debug.Log("hurt me");
    }

}
