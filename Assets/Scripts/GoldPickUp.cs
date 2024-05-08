using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour
{
    private System.Random random;
    int soulsForPickUp;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        random = new System.Random(DateTime.Now.Millisecond);
        soulsForPickUp = random.Next(0, 101);
        Debug.Log("random soul amount: " + soulsForPickUp);
        soulsForPickUp = (soulsForPickUp - soulsForPickUp % 10) + 10;
        Debug.Log("Rounded soul amount: " + soulsForPickUp);
        audioManager.PlayDelayed(audioManager.pickup);
        if (collision.tag == "Player" && GlobalVariables.isAlive)
        {
            
            FindObjectOfType<GameSession>().AddSoul(soulsForPickUp);
            Destroy(gameObject);
        }
    }
}
