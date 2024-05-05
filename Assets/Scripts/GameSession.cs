using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    
    float levelLoadDelay = 1.5f;
    [SerializeField]TextMeshProUGUI livesText;
    [SerializeField]TextMeshProUGUI soulText;
    [SerializeField]TextMeshProUGUI attackText;
    [SerializeField]TextMeshProUGUI critHitChanceText;
    [SerializeField]TextMeshProUGUI critHitDMGText;

    public int maxHealth = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
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
        critHitChanceText.text = GlobalVariables.critHitChance.ToString() + " %";
        critHitDMGText.text = GlobalVariables.critHitDMG.ToString() + " %";
    }

    private void Update()
    {
        livesText.text = GlobalVariables.lives.ToString();
        soulText.text = GlobalVariables.soul.ToString();
        attackText.text = GlobalVariables.damage.ToString();
        critHitChanceText.text = GlobalVariables.critHitChance.ToString() + " %";
        critHitDMGText.text = GlobalVariables.critHitDMG.ToString() + " %";
        
        for (int i = 0; i < hearts.Length; i++)
        {
            //Show full or empty heart based on current health
            if (i < Convert.ToInt32(livesText.text))
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            // Show or hide the heart image based on max health
            hearts[i].enabled = i < maxHealth;
        }

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
        GlobalVariables.newLevel = false;
        GlobalVariables.isAlive = true;
    }

    IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        GlobalVariables.newLevel = true;
        GlobalVariables.isAlive = true;
    }
}
