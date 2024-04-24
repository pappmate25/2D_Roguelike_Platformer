using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    float movementspeed = 6f;
    float jumpspeed = 13f;
    float climbspeed = 6f;
    [SerializeField] Vector2 deathKick = new Vector2(15f, 15f);

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D shoeCollider;
    float gravityScaleAtStart;

    bool isAlive = true;

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
        if (!isAlive)
        {
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }


    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        if (value.isPressed && shoeCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || value.isPressed && shoeCollider.IsTouchingLayers(LayerMask.GetMask("Hidden Platform")))
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

    void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            rb.velocity = deathKick;
        }
    }
}
