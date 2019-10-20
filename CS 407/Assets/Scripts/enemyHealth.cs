using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public int sampleHealth;
    public int sampleMaxHealth;
    public double width;
    public double height;
    // Start is called before the first frame update
    void Start()
    {
        sampleHealth = 100;
        sampleMaxHealth = 100;
        if (transform.parent.name == "Enemy_type1(Clone)" || transform.parent.name == "Enemy(Clone)" || transform.parent.name == "Enemy_type1" || transform.parent.name == "Enemy")
        {
            width = 0.0857904;
            height = 0.09419625;
        }
        else if (transform.parent.name == "Enemy3" || transform.parent.name == "Enemy4") {
            width = 0.0857904;
            height = 0.09419625;
        }
        else if (transform.parent.name == "Enemy_type2(Clone)" || transform.parent.name == "Enemy_type2")
        {
            width = 0.48651;
            height = 0.53418;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //receive the player's health from their stats array
        double enemyHealth = (double)transform.parent.GetComponent<EnemyController>().health;

        //receive the player's max health from their stats array
        double enemyMaxHealth = (double)transform.parent.GetComponent<EnemyController>().maxHealth; ;

        //Calculate the percent and multiply it by the initial width of the bar (e.g. at 100% it will equal 223.813)
        double percent = enemyHealth/ enemyMaxHealth * width;

        //set new size of the health bar
        transform.localScale = new Vector2((float)percent, (float)height);

        if(sampleHealth <= 0){
            Destroy(gameObject);
            GameObject.Find("Player").GetComponent<PlayerController>().IncreaseScore(1);
        }
    }

    public void TakeDamage(int damage){
        sampleHealth -= damage;
        //Debug.Log("hurt me");
    }
}
