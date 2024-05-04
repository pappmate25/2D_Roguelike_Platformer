using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour
{
    int soulsForPickUp = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && GlobalVariables.isAlive)
        {
            FindObjectOfType<GameSession>().AddSoul(soulsForPickUp);
            Destroy(gameObject);
        }
    }
}
