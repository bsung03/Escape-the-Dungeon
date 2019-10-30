using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Rigidbody2D rb2;

    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    Vector2 lookDir;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1f)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb2.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);


        lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb2.rotation = angle;
    }

    void Jump()
    {
        Debug.Log(lookDir);
        Debug.Log(movement);
        // transform.position = new Vector3(lookDir.x, lookDir.y).normalized * 5 ;

        //rb2.transform.Translate(movement.x * 2 , movement.y * 2, 0) ;

        rb.transform.Translate(rb2.position.x * 2, rb2.position.y * 2, 0);

        //transform. = new Vector2(movement.x * 2, movement.y * 2);

    }
}
