using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    private BoxCollider2D boxCollider;

    private Vector3 moveDelta;

    public float moveSpeed = 5f;

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


        transform.Translate(moveSpeed * moveDelta * Time.deltaTime);

    }
}
