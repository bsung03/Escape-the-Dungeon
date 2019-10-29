using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public GameObject bombPrefab;

    public GameObject shoots;
    public GameObject bombs;

    private AudioSource a;
    private AudioSource b;


    public float bulletForce = 20f;

    private float timeBtwAttack = 0;
    public float cooldown;
    public float skillcooldown;
    private float timeskill = 0;

    void Start()
    {
        a = shoots.GetComponent<AudioSource>();
        b = bombs.GetComponent<AudioSource>();


    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale == 1f)
        {
            if (Time.time > timeBtwAttack) {
                Shoot();
                timeBtwAttack = Time.time + cooldown;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1f)
        {
            if (Time.time > timeskill)
            {
                Bomb();
                timeskill = Time.time + skillcooldown;
            }
        }
    }

    void Shoot()
    {
        a.Play();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
    void Bomb()
    {
        b.Play();
        GameObject bomb = Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
