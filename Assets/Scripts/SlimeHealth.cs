using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHealth : MonoBehaviour
{
    public GameObject[] soulsDrop;
    bool isDead = false;
    bool isSoulDropped = false;
    int maxHealth = 100;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Update()
    {
        if (isDead && !isSoulDropped)
        {
            SoulsDrop();
            isSoulDropped = true;  
            SlimeDeath();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            Destroy(collision.gameObject);
            DamagingSlime();
            if(maxHealth <= 0)
            {
                isDead = true;
            }
            Debug.Log("slime health is: " + maxHealth);
        }
    }

    private void SoulsDrop()
    {
        for (int i = 0; i < soulsDrop.Length; i++)
        {
            Instantiate(soulsDrop[i], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    private void DamagingSlime()
    {
        if(maxHealth > 0)
        {
            maxHealth -= GlobalVariables.damage;
            audioManager.PlayOneShot(audioManager.slimeHit);
        }
        else
        {
            isDead = true;
        }
    }

    private void SlimeDeath()
    {
        Destroy(gameObject);
    }
}
