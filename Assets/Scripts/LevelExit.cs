using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{
    float levelLoadDelay = 1.5f;
    Animator animator;
    public GameObject playerObject;
    Player player;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = playerObject.GetComponent<Player>();
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GlobalVariables.isAlive)
        {
            StartCoroutine(LoadNextLevel());
            animator.SetTrigger("Exit trigger");
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            audioManager.PlayDelayed(audioManager.gameStart);
            GlobalVariables.doneOneRun = true;
            nextSceneIndex = 0;
            player.SavePlayerStats();            
        }
        GlobalVariables.newLevel = true;
        SceneManager.LoadScene(nextSceneIndex);      
    }
}
