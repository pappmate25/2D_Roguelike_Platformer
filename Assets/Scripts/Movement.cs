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
    Vector2 deathKick = new Vector2(1f, 15f);
    
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D shoeCollider;
    float gravityScaleAtStart;

    bool isAlive = true;

    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;

    float dodgeSpeed = 10f;

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
        Shoot();
        
        
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

        if (value.isPressed && shoeCollider.IsTouchingLayers(LayerMask.GetMask("Ground","Hidden Platform")))
        {
            rb.velocity += new Vector2(0f, jumpspeed);
        }

    }
    void OnDodge(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        if (value.isPressed && shoeCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Hidden Platform")))
        {
            rb.velocity += new Vector2(dodgeSpeed, 0f);
        }

    }
    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        Instantiate(arrow, bow.position, transform.rotation);
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
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            rb.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Shooting");
        }
    }
}
