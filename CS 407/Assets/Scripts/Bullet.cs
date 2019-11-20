using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    public int damage;
    public float range;

    void Start()
    {
        damage = 1;
        range = .3f;
    }
    void Update()
    {
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
