using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using st;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    public int damage;
    public float range;

    void Start()
    {
        
    }
    void Update()
    {
        damage = Shooting.damage;
        range = Shooting.range;
        Range();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, .4f);
            Destroy(gameObject);
            collision.GetComponent<EnemyController>().TakeDamage(damage);
            Debug.Log("damge: " + damage);
        }
        else if (collision.tag == "Blocking")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, .4f);
            Destroy(gameObject);
        }
    }
    void Range()
    {
        Destroy(gameObject, range);
    }
}
