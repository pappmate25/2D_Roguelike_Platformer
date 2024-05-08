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
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    BoxCollider2D shoeCollider;
    float gravityScaleAtStart;

    //dodge
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.8f;
    private float dashTimeLeft;
    private float lastDash = -10f;
    private bool isDashing = false;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScaleAtStart = rb.gravityScale;
        animator = GetComponent<Animator>();
        shoeCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!GlobalVariables.isAlive || GlobalVariables.isShopOpen)
        {
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= (lastDash + dashCooldown))
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            lastDash = Time.time;
        }

        if (isDashing)
        {
            Dash();
        }
    }


    void OnMove(InputValue value)
    {

        if (!GlobalVariables.isAlive)
        {
            
            return;
        }
        moveInput = value.Get<Vector2>();
        audioManager.PlayDelayed(audioManager.walking);
    }

    void OnJump(InputValue value)
    {
        
        if (!GlobalVariables.isAlive || GlobalVariables.isShopOpen)
        {
            return;
        }

        if (value.isPressed && shoeCollider.IsTouchingLayers(LayerMask.GetMask("Ground","Hidden Platform")))
        {
            audioManager.PlayOneShot(audioManager.JUMP);
            rb.velocity += new Vector2(0f, jumpspeed);
        }
    }
    private void Dash()
    {
        
        if (dashTimeLeft > 0)
        {
            
            gameObject.layer = LayerMask.NameToLayer("InvinciblePlayer");       
            rb.velocity = new Vector2(transform.localScale.x * dashSpeed, rb.velocity.y);
            dashTimeLeft -= Time.deltaTime;
            animator.SetTrigger("Dodge");

        }
        else
        {
            isDashing = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            gameObject.layer = LayerMask.NameToLayer("Player");
            animator.ResetTrigger("Dodge");
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
            audioManager.PlayDelayed(audioManager.ladderClimb);
            rb.gravityScale = gravityScaleAtStart;
            animator.SetBool("isClimbing", false);
        }
    }
}
