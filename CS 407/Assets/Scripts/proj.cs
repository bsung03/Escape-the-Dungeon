using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class proj : MonoBehaviour
{
   public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioSource shoots;
    public float bulletForce = 15f;
    public Vector2 relativePoint;
    public GameObject player;
    public GameObject key;
    public TextMeshPro health_text;
    public Animator animator;
    float health;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DecideAttack", 2, 2.0f);
        InvokeRepeating("ChangeTarget", 2, 5.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health_text == null)
        {
            GameObject t = GameObject.FindWithTag("BossHealthText");
            if (t == null)
            {
                print("Boss Text not found");
            }
            else
            {
                print("Boss text found");
                health_text = t.GetComponent<TextMeshPro>();
            }
        }

        if (player == null)
        {
            player = this.GetComponent<EnemyAI>().target.gameObject;
        }
        relativePoint = transform.InverseTransformPoint(player.transform.position);
        if (relativePoint.x < 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (relativePoint.x > 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

        health = this.GetComponent<EnemyController>().health;
        if (health <= 0)
        {
            health_text.SetText("Boss Defeated!");
        }
        else
        {
            health_text.SetText("Boss Health: " + health.ToString());
        }



    }
    void ChangeTarget()
    {

        this.GetComponent<EnemyAI>().moving = true;
        Vector3 myVector = new Vector3(UnityEngine.Random.Range(-10.0f, 8), UnityEngine.Random.Range(-9.6f, 2.3f), UnityEngine.Random.Range(-2, -2));
        GameObject emptyGO = new GameObject();
        Transform newTransform = emptyGO.transform;
        newTransform.position = myVector;
        this.GetComponent<EnemyAI>().target = newTransform;

    }

    void DecideAttack()
    {
        if(health > 0)
        {
            int r = Random.Range(0, 5);
            if (r == 0)
                Attack1();
            else if (r == 1)
                Attack2();
            else if (r == 2)
                Attack3();
            else
                ShootAtPlayer();
        }

    }


    void Attack1()
    {
        ShootDirection(this.transform.right);
        ShootDirection(this.transform.up);
        ShootDirection(-this.transform.up);
        ShootDirection(-this.transform.right);
    }
    void Attack2()
    {
        ShootDirection(this.transform.right+ this.transform.up);
        ShootDirection(this.transform.up - this.transform.right);
        ShootDirection(-this.transform.up - this.transform.right);
        ShootDirection(this.transform.right- this.transform.up);
    }

    void Attack3()
    {
        ShootDirection(this.transform.right);
        ShootDirection(this.transform.up);
        ShootDirection(-this.transform.up);
        ShootDirection(-this.transform.right);
        ShootDirection(this.transform.right + this.transform.up);
        ShootDirection(this.transform.up - this.transform.right);
        ShootDirection(-this.transform.up - this.transform.right);
        ShootDirection(this.transform.right - this.transform.up);
    }

    void ShootAtPlayer()
    {
        shoots.Play();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector3 dir = (player.transform.position - this.transform.position).normalized;
        bullet.GetComponent<EnemyBullet>().player = player;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, 5.0f);
    }

    void ShootDirection(Vector3 dir)
    {
        shoots.Play();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<EnemyBullet>().player = player;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, 5.0f);
    }

    void FinishDeath()
    {
        //Destroy(this.gameObject);
    }
}
