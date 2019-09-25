using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLooking : MonoBehaviour
{

    private Vector2 current_Mouse_Look;
    private Vector3 Player_current_position;
    private Vector3 Mouse_current_position;

    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Mouse_current_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //current_Mouse_Look = new Vector2(Input.GetAxis("Horizontal2"),Input.GetAxis("Vertical2"));
        Player_current_position = transform.position;

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, Mouse_current_position, 0f, LayerMask.GetMask("collect"));
        if(hit != null){
            Debug.Log("hithit");
        }
    }
}
