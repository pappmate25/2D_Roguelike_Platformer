using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    CapsuleCollider2D bodyCollider;
    Animator animator;
    Rigidbody2D rb;
    Vector2 deathKick = new Vector2(1f, 15f);

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (!GlobalVariables.isAlive)
        {
            return;
        }
        Die();
       
    }

    void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")) && !GlobalVariables.isInvincible)
        {
            GlobalVariables.isAlive = false;
            animator.SetTrigger("Dying");
            rb.linearVelocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            audioManager.PlayOneShot(audioManager.character_hurt);
        }
    }
}
