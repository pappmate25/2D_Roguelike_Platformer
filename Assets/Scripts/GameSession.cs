using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    
    float levelLoadDelay = 1.5f;
    [SerializeField]TextMeshProUGUI livesText;
    [SerializeField]TextMeshProUGUI goldText;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = GlobalVariables.lives.ToString();
        goldText.text = GlobalVariables.soul.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if(GlobalVariables.lives > 1)
        {
            StartCoroutine(TakeLife());
        }
        else
        {
            StartCoroutine(ResetGameSession());
        }
    }

    public void AddGold(int goldToAdd)
    {
        GlobalVariables.soul += goldToAdd;
        goldText.text = GlobalVariables.soul.ToString();
    }

    IEnumerator TakeLife()
    {
        GlobalVariables.lives--;
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = GlobalVariables.lives.ToString();
        GlobalVariables.isAlive = true;
    }

    IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        GlobalVariables.isAlive = true;
    }
}
