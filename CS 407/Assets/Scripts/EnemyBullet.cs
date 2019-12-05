using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;

    public int damage;
    public GameObject player;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, .4f);
            Destroy(gameObject);
            //collision.GetComponent<EnemyController>().TakeDamage(damage);
            player.GetComponent<PlayerController>().SendMessage("DamagePlayer", 15);
        }
        else if (collision.tag == "Blocking")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, .4f);
            Destroy(gameObject);
        }
    }
}
