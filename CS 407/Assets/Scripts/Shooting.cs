using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace st
{
    public class Shooting : MonoBehaviour
    {
        public GameObject hitEffect;
        public static int damage;
        public static float range;

        public Transform firePoint;
        public GameObject bulletPrefab;

        public GameObject bombPrefab;

        public AudioSource shoots;
        public AudioSource bombs;
        public AudioSource rolls;

        public float bulletForce = 10f;

        private float timeBtwAttack = 0;
        public static float cooldown;
        public static float skillcooldown;
        public static float skillcooldown1;

        private float timeskill = 0;
        private float timeskill1 = 0;
        private bool sk1, sk2;
        public Image imagecooldown1;
        public Image imagecooldown2;

        public static bool sskill;


        public Camera cam;

        Vector3 movement;
        Vector3 mousePos;

        Vector2 lookDir;

        public GameObject rb1;
        public BoxCollider2D rb2;

        private float slideSpeed;
        Vector2 d;

        public Animator animator;

        private State state;
        private enum State
        {
            Normal, DodgeRollSliding,
        }

        void Start()
        {
            damage = 1;
            range = .3f;
            cooldown = 1f;
            skillcooldown = 5f;
            skillcooldown1 = 3f;
            sskill = false;

            switch (state)
            {
                case State.Normal:
                    roll();
                    break;
                case State.DodgeRollSliding:
                    roll2();
                    break;
            }

        }
        void Update()
        {
            bulletPrefab.GetComponent<Bullet>().damage = damage;
            bulletPrefab.GetComponent<Bullet>().range = range;

            if (cam == null)
            {
                cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            }
            //movement.x = Input.GetAxisRaw("Horizontal");
            //movement.y = Input.GetAxisRaw("Vertical");

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButton(0) && Time.timeScale == 1f)
            {
                if (Time.time > timeBtwAttack)
                {
                    Shoot();
                    timeBtwAttack = Time.time + cooldown;
                }
            }
            if (Input.GetMouseButtonDown(1) && Time.timeScale == 1f)
            {
                if (Time.time > timeskill)
                {
                    sk1 = true;
                    Bomb();
                    timeskill = Time.time + skillcooldown;
                    imagecooldown1.fillAmount = 0;
                }
  
            }
            if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1f)
            {
                if (Time.time > timeskill1)
                {
                    sk2 = true;
                    roll();
                    timeskill1 = Time.time + skillcooldown1;
                    imagecooldown2.fillAmount = 0;
                }
            }
            roll2();

            if (sk1)
            {
                imagecooldown1.fillAmount += 1 / skillcooldown * Time.deltaTime;
            }
            if (sk2)
            {
                imagecooldown2.fillAmount += 1 / skillcooldown1 * Time.deltaTime;
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
            Debug.Log(range);

            shoots.Play();
            
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

            if (sskill)
            {
                GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                rb1.AddForce(-firePoint.up * bulletForce, ForceMode2D.Impulse);

                GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                rb2.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

                GameObject bullet3 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                rb3.AddForce(-firePoint.right * bulletForce, ForceMode2D.Impulse);
            }
        }
        void Bomb()
        {
            bombs.Play();
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
            rolls.Play();
            slideSpeed = 40f;
            d = (mousePos - rb2.transform.position).normalized;
            state = State.DodgeRollSliding;
            rb2.tag = "roll";
            animator.SetBool("roll", true);
        }
        void roll2()
        {
            rb2.transform.position += new Vector3(d.x, d.y).normalized * slideSpeed * Time.deltaTime;
            slideSpeed -= slideSpeed * 3f * Time.deltaTime;
            if (slideSpeed < 5f)
            {
                rb2.tag = "Player";
                animator.SetBool("roll", false);
                state = State.Normal;
            }
        }
    }
}