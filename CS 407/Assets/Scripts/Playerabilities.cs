using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerabilities : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    private Vector2 moveDelta;
    private bool dashing;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        dashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        handleDash();
    }

    private void handleDash()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0f || y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                moveDelta = new Vector2(x, y);
                moveDelta.Normalize();
                dashing = true;
            }
        }

        if (dashing == true)
        {
            if (dashTime <= 0)
            {
                dashTime = startDashTime;
                rigidbody.velocity = Vector2.zero;
                dashing = false;
            }
            else
            {
                dashTime -= Time.deltaTime;
                rigidbody.velocity = moveDelta * dashSpeed;
            }
        }
    }
}
