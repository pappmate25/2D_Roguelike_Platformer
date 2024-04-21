using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject objectToDestroy;
    int currentLevel;

    private void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "obstacle")
        {
            Destroy(objectToDestroy);
            SceneManager.LoadScene(currentLevel);
        }
    }
}
