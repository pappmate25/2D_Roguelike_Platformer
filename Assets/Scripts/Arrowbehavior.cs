using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrowbehavior : MonoBehaviour
{
    Rigidbody2D rb;
    float arrowSpeed = 20f;
    Movement player;
    float xSpeed;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Movement>();
        xSpeed = player.transform.localScale.x * arrowSpeed;
    }

   
    void Update()
    {
        rb.linearVelocity = new Vector2(xSpeed, 0f);
    }  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
