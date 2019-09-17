using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    private BoxCollider2D boxCollider;

    private Vector3 moveDelta;

    public float moveSpeed = 5f;

    private RaycastHit2D hit;

    

    // Start is called before the first frame update
    void Start()
    {
       boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x,y,0);

        if(moveDelta.x < 0){
            transform.localScale = Vector3.one;
        }else if(moveDelta.x > 0){
            transform.localScale = new Vector3(-1,1,1);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveSpeed * moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null)
        {
            transform.Translate(0,moveDelta.y * Time.deltaTime * moveSpeed, 0);
        }
        else
        {
            Debug.Log("blocker");
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs( moveSpeed * moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime * moveSpeed, 0,0);
        }
        else
        {
            Debug.Log("blocker");
        }

    }
}
