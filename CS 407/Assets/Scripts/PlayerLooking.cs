using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLooking : MonoBehaviour
{

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Vector3 interactPos;
    public float interactRange;
    public LayerMask interactLayerMask;
    private Vector3 Mouse_current_position;
    private Vector3 look_direction;
    public int damage;

    void Update()
    {
        if(timeBtwAttack <= 0){

            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("space");
                Mouse_interact_pos();
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(interactPos, interactRange, interactLayerMask);
                for(int i = 0;i < enemiesToDamage.Length; i++)
                {
                    Debug.Log("attack");
                    enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(damage);
                }
            }

            timeBtwAttack = startTimeBtwAttack;
        }else{
            timeBtwAttack -= Time.deltaTime;
        }

        
    }

    private void Mouse_interact_pos()
    {
        Mouse_current_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Mouse_current_position.z = transform.position.z;
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
