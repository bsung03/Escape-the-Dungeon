using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerabilities : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private Vector2 moveDelta;
    private bool dashing;
    private bool slashing;
    public float interactRange;
    public LayerMask interactLayerMask;
    private Vector3 Mouse_current_position;
    private Vector3 look_direction;
    private Vector3 slashStartPos;
    private Vector3 slashPos;
    public float startSlashTime;
    private float slashTime;
    public int damage;
    private Vector3 initTransformSlashPos;
    private float slashDegree;
    public float slashSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        dashing = false;
        slashing = false;
        slashTime = startSlashTime;
    }

    // Update is called once per frame
    void Update()
    {
        handleDash();
        handleSlashing();
    }

    private void handleSlashing(){
        
        if(Input.GetKeyDown(KeyCode.C)){
            slashing = true;
            Mouse_interact_pos();
            slashPos = slashStartPos;
            initTransformSlashPos = transform.position;
        }

        if(slashing){
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(slashPos, interactRange, interactLayerMask);
            Vector3 dir = slashPos - transform.position;
            dir = Quaternion.Euler(0f,0f,slashSpeed) * dir;
            slashPos = dir + transform.position;
            slashDegree += slashSpeed;

            for(int i = 0;i < enemiesToDamage.Length; i++)
                {
                    //Debug.Log("attack");
                    enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(damage);
                }

            if(slashDegree >= 360f){
                slashing = false;
                slashDegree = 0f;
            }
        }
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

    private void Mouse_interact_pos()
    {
        Mouse_current_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Mouse_current_position.z = transform.position.z;
        look_direction = Mouse_current_position - transform.position;
        look_direction.Normalize();
        slashStartPos = transform.position + look_direction;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(slashPos, interactRange);
    }
}
