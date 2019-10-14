using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int health = 3;
    public int maxHealth = 3;
    public GameObject gold;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && !dead)
        {

            KillEnemy();
            dead = true;
        }
    }

    private void KillEnemy()
    {
        if (dead)
            return;
        if(this.gameObject.tag == "Boss")
        {
            this.GetComponent<Animator>().SetTrigger("Kill");
            this.GetComponent<Animator>().SetBool("Dead", true);
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            this.GetComponent<EnemyAI>().enabled = false;
            Destroy(gameObject,3);
            //Make a gold ovject where enemy dies
            Instantiate(gold, transform.position -  transform.up, Quaternion.identity, null);
            Instantiate(gold, transform.position + transform.up, Quaternion.identity, null);
            SkeletonBoss skeleton = this.gameObject.GetComponent("SkeletonBoss") as SkeletonBoss;
            if(skeleton != null)
                Instantiate(skeleton.key, transform.position + transform.right, Quaternion.identity, null);
            BullManager bull = this.gameObject.GetComponent("BullManager") as BullManager;
            if (bull != null)
                Instantiate(bull.key, transform.position + transform.right, Quaternion.identity, null);

            //Increase player's score
            GameObject.Find("Player").GetComponent<PlayerController>().IncreaseScore(2);

            //Grant player experience
            GameObject.Find("Player").GetComponent<PlayerController>().addExperience(30);
        }
        else
        {
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
