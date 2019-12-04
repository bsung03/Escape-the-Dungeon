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
        Mouse_interact_pos();
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(interactPos, interactRange, interactLayerMask);
        if(timeBtwAttack <= 0){

            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("space");
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
        Mouse_current_position.z = -2;
        look_direction = Mouse_current_position - transform.position;
        look_direction.Normalize();
        interactPos = transform.position + look_direction;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactPos, interactRange);
    }

    public void increaseBaseDamage(){
        damage += 1;
    }

    public void decreaseTimeBTWAttack(){
        timeBtwAttack = (timeBtwAttack / 4) * 3;
    }
}
