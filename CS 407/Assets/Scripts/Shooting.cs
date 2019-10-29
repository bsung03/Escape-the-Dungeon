using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public AudioSource shoots;

    public float bulletForce = 20f;

    private float timeBtwAttack = 0;
    public float cooldown;

    void Update()
    {
        if (Time.time > timeBtwAttack)
        {
            if (Input.GetButtonDown("Fire1") && Time.timeScale == 1f)
            {
                shoots.Play();
                Shoot();
                timeBtwAttack = Time.time + cooldown;

            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
