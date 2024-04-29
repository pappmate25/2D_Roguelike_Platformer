using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{
    float levelLoadDelay = 1f;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GlobalVariables.isAlive)
        {
            StartCoroutine(LoadNextLevel());     
        }
    }
    
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);      
    }
}
