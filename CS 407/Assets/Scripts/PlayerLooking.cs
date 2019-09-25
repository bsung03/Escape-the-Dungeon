using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLooking : MonoBehaviour
{

    public Vector3 interactPos;
    public float interactRange;
    public LayerMask interactLayerMask;
    private Vector3 Mouse_current_position;
    private Vector3 look_direction;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("space");
            Mouse_interact_pos();
            Collider2D[] interact = Physics2D.OverlapCircleAll(interactPos, interactRange, interactLayerMask);
            for(int i = 0;i < interact.Length; i++)
            {
                Debug.Log("interact");
            }
        }
    }

    private void Mouse_interact_pos()
    {
        Mouse_current_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        look_direction = Mouse_current_position - transform.position;
        look_direction.Normalize();
        interactPos = transform.position + look_direction;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactPos, interactRange);
    }
}
