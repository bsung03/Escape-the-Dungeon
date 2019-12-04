using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int damage;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject, .4f);
            collision.GetComponent<EnemyController>().TakeDamage(damage);
        }
        else if (collision.tag == "Blocking")
        {
            Destroy(gameObject);
        }
    }
}
