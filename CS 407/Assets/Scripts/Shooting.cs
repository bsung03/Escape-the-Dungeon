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
    public float skillcooldown1;
    private float timeskill1 = 0;

    public Camera cam;

    Vector3 movement;
    Vector3 mousePos;

    Vector2 lookDir;

    public GameObject rb1;
    public BoxCollider2D rb2;



    void Start()
    {
        a = shoots.GetComponent<AudioSource>();
        b = bombs.GetComponent<AudioSource>();


    }
    void Update()
    {
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);



        if (Input.GetMouseButtonDown(0) && Time.timeScale == 1f)
        {
            if (Time.time > timeBtwAttack) {
                Shoot();
                timeBtwAttack = Time.time + cooldown;
            }
        }
        if (Input.GetMouseButtonDown(1) && Time.timeScale == 1f)
        {
            if (Time.time > timeskill)
            {
                Bomb();
                timeskill = Time.time + skillcooldown;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1f)
        {
            if (Time.time > timeskill1)
            {
                roll();
                timeskill1 = Time.time + skillcooldown1;
            }
        }
    }
    void FixedUpdate()
    {
        /*
        lookDir = mousePos - rb1.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //rb1.rotation = angle;
        rb1.transform.Rotate(0, 0, lookDir.x);
        */
        lookDir = mousePos - rb1.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb1.transform.rotation = Quaternion.Slerp(rb1.transform.rotation, rotation, 500f * Time.deltaTime);
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
    void Jump()
    {
        Debug.Log(lookDir);
        Debug.Log(movement);
        // transform.position = new Vector3(lookDir.x, lookDir.y).normalized * 5 ;

        //rb2.transform.Translate(movement.x * 2 , movement.y * 2, 0) ;

        rb2.transform.Translate(lookDir.x, lookDir.y, 0);

        //transform. = new Vector2(movement.x * 2, movement.y * 2);

    }

    void roll()
    {
        float slideSpeed = 400f;
        Vector2 d = (mousePos - rb2.transform.position).normalized;
        rb2.transform.position += new Vector3(d.x, d.y).normalized * slideSpeed * Time.deltaTime;
        slideSpeed -= slideSpeed * 5f * Time.deltaTime;
    }

}
