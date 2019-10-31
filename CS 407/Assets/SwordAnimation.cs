using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("Speed",0f);

    }

    // Update is called once per frame
    void Update()
    {
        Walking();
        if(Input.GetMouseButtonDown(0)){
            animator.Play("Attack");
        }
    }

    private void Walking()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0f || y != 0f)
        {
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }
}
