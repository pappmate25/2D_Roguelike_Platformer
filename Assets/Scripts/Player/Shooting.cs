using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GlobalVariables.isAlive || GlobalVariables.isShopOpen)
        {
            return;
        }
        Shoot();
    }

    void OnFire(InputValue value)
    {
        if (!GlobalVariables.isAlive || GlobalVariables.isShopOpen)
        {
            return;
        }
        Instantiate(arrow, bow.position, transform.rotation);
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Shooting");
            audioManager.PlayOneShot(audioManager.bow);
        }
    }
}
