using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    
    float levelLoadDelay = 1.5f;
    [SerializeField]TextMeshProUGUI livesText;
    [SerializeField]TextMeshProUGUI soulText;
    [SerializeField]TextMeshProUGUI attackText;

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
        soulText.text = GlobalVariables.soul.ToString();
        attackText.text = GlobalVariables.damage.ToString();
    }

    private void Update()
    {
        livesText.text = GlobalVariables.lives.ToString();
        soulText.text = GlobalVariables.soul.ToString();
        attackText.text = GlobalVariables.damage.ToString();
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

    public void AddSoul(int soulToAdd)
    {
        GlobalVariables.soul += soulToAdd;
        soulText.text = GlobalVariables.soul.ToString();
    }

    IEnumerator TakeLife()
    {
        GlobalVariables.lives--;
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = GlobalVariables.lives.ToString();
        GlobalVariables.firstRun = false;
        GlobalVariables.isAlive = true;
    }

    IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        GlobalVariables.firstRun = true;
        GlobalVariables.isAlive = true;
    }
}
