using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 3f;
    public float DashSpeed = 6f;
    public float JumpForce = 15f;

    private Rigidbody2D rb;

    private bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Player Movement
        if (!isDashing)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * MoveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * DashSpeed, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }

        if (Input.GetButton("Dash"))
        {
            isDashing = true;
        }
        else
        {
            isDashing = false;
        }

        //Sprite Flip
        if(rb.velocity.x > 0)
        {
            GetComponent<Animator>().SetBool("flip", false);
        }
        else if (rb.velocity.x < 0)
        {
            GetComponent<Animator>().SetBool("flip", true);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        //Run
        if (rb.velocity.x != 0)
        {
            if (!isDashing)
            {
                GetComponent<Animator>().SetInteger("state", 1);
            }
            else
            {
                GetComponent<Animator>().SetInteger("state", 4);
            }
        }
        else
        {
            GetComponent<Animator>().SetInteger("state", 0);
        }

        //Jump / Fall
        if(rb.velocity.y > 0.1f)
        {
            GetComponent<Animator>().SetInteger("state", 2);
        }
        else if(rb.velocity.y < -0.1f)
        {
            GetComponent<Animator>().SetInteger("state", 3);
        }
    }
}