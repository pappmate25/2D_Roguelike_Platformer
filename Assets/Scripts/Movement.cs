using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float movementspeed = 6f;
    [SerializeField] float jumpspeed = 13f;
    [SerializeField] float climbspeed = 6f;

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D shoeCollider;
    float gravityScaleAtStart;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScaleAtStart = rb.gravityScale;
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        shoeCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }
    

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && shoeCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || value.isPressed && shoeCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.velocity += new Vector2(0f, jumpspeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * movementspeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;       
        animator.SetBool("isRunning", playerHasHorizontalSpeed);


    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;  //Mathf.Epsilon tulajdonképpen = 0

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (shoeCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbspeed);
            rb.velocity = climbVelocity;
            rb.gravityScale = 0f;
            animator.SetBool("isClimbing", true);
        }
        else
        {
            rb.gravityScale = gravityScaleAtStart;
            animator.SetBool("isClimbing", false);
        }


    }





}
