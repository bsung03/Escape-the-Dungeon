using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int health = 3;
    public int maxHealth = 3;
    public GameObject gold;
    public GameObject wave;
    bool dead = false;

    public Vector2 relativePoint;
    bool boss;
    float dist;
    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("Attack", 1, 2.5f);
        boss = this.gameObject.tag == "Boss";
        this.GetComponent<EnemyAI>().moving = false;
        //Grab GM for future access to wave number to adjust difficulty
        wave = GameObject.Find("GM");

        //Adjust health and max health based on wave number
        health += wave.GetComponent<Spawner>().waveNumber;

        maxHealth += wave.GetComponent<Spawner>().waveNumber;
    }

    // Update is called once per frame
    void Update()
    {
        relativePoint = transform.InverseTransformPoint(this.gameObject.GetComponent<EnemyAI>().target.position);
        if (relativePoint.x < 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (relativePoint.x > 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (health <= 0 && !dead)
        {

            KillEnemy();
            dead = true;
            this.GetComponent<EnemyAI>().moving = false;
        }
    }
    public void Attack()
    {
        if (health <= 0)
            return;

        dist = Vector3.Distance(this.transform.position, this.gameObject.GetComponent<EnemyAI>().target.position);
        print("Dist: " + dist.ToString());
        if (dist < 2.4f)
        {
            this.GetComponent<Animator>().SetBool("Walking", false);
            this.GetComponent<EnemyAI>().moving = false;
            this.GetComponent<Animator>().SetTrigger("Attack");
        }
        else
        {
            this.GetComponent<Animator>().SetBool("Walking", true);
            this.GetComponent<EnemyAI>().moving = true;
        }
    }
    private void KillEnemy()
    {
        if (dead)
            return;
        this.GetComponent<Animator>().SetTrigger("Kill");
        this.GetComponent<Animator>().SetBool("Dead", true);
        this.GetComponent<EnemyAI>().enabled = false;
        this.GetComponent<Pathfinding.Seeker>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        if (boss)
        {
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
            Destroy(gameObject,2);
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
    public void HitPlayer()
    {

        dist = Vector3.Distance(this.transform.position, this.gameObject.GetComponent<EnemyAI>().target.position);
        if (dist < 2f)
        {
            print("Enemy Hit");
            this.GetComponent<EnemyAI>().target.gameObject.GetComponent<PlayerController>().SendMessage("DamagePlayer", 10);

        }

    }
}
