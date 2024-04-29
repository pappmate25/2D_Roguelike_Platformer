using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float dashSpeed = 20f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 1f;
    private float dashTimeLeft;
    private float lastDash = -10f;
    private bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GlobalVariables.isAlive)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= (lastDash + dashCooldown))
        {
            StartDash();
        }

        if (isDashing)
        {
            ContinueDash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        lastDash = Time.time;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, rb.velocity.y);
        animator.SetTrigger("Dodge");
    }

    private void ContinueDash()
    {
        if (dashTimeLeft > 0)
        {
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            EndDash();
        }
    }

    private void EndDash()
    {
        isDashing = false;
        rb.velocity = new Vector2(0, rb.velocity.y);  // Optionally reset velocity or manage it according to the animation
    }
}
