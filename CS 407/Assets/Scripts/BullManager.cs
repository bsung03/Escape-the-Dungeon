using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BullManager : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public Vector2 relativePoint;
    public GameObject key;
    public TextMeshPro health_text;
    bool started = false;
    bool walking = false;
    int health;
    private float thrust = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateBoss", 2,2);
        this.GetComponent<EnemyAI>().moving = false;

    }

    // Update is called once per frame
    void Update()
    {
        relativePoint = transform.InverseTransformPoint(player.transform.position);
        if (relativePoint.x < 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (relativePoint.x > 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
       
        health = this.GetComponent<EnemyController>().health;
        if ( health <= 0)
        {
            health_text.SetText("Boss Defeated!");
        }
        else
        {
            health_text.SetText("Boss Health: " + health.ToString());
        }
    }
    public void StopWalking()
    {
        walking = false;
        print("Stop walkig");
        this.GetComponent<EnemyAI>().moving = false;
    }
    public void StartWalk()
    {
        this.GetComponent<EnemyAI>().moving = true;
        walking = true;
        animator.SetBool("Walking", walking);
    }
    public void UpdateBoss()
    {
        if (health <= 0)
            return;
        float dist = Vector3.Distance(player.transform.position, transform.position);

        print("Dist: " + dist.ToString());
        if (dist > 5)
        {
            if (started)
            {
                walking = true;
                animator.SetBool("Walking", walking);

                this.GetComponent<EnemyAI>().moving = true;
                animator.ResetTrigger("Start");
                started = false;
            }
            else
            {
                started = true;
                animator.SetTrigger("Start");
                walking = false;
                this.GetComponent<EnemyAI>().moving = false;

            }
        }
        else
        {
            walking = false;
            this.GetComponent<EnemyAI>().moving = false;
            animator.SetBool("Walking", walking);
            int r = Random.Range(0, 2);
            if(r == 0)
            {
                animator.SetTrigger("Attack1");
            }
            else
            {
                animator.SetTrigger("Attack3");
            }
        }
    }

    public void HitPlayer()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < 3.2f)
        {
            print("Boss Hit");
            player.GetComponent<PlayerController>().SendMessage("DamagePlayer", 20);
            player.GetComponent<Rigidbody2D>().AddForce(transform.right * thrust);
        }

    }
}
