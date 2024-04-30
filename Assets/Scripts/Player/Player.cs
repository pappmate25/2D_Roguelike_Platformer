using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int currentLevel;
    public int lives;
    public int damage = 10;
    public int soul;

    public void SavePlayer()
    {
        soul = GlobalVariables.soul;
        lives = GlobalVariables.lives;
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        SaveSystem.SavePlayer(this);
        Debug.Log("The game is saved, current level: " + currentLevel);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        currentLevel = data.currentLevel;
        lives = data.lives;
        damage = data.damage;
        soul = data.soul;
        GlobalVariables.soul = soul;
        GlobalVariables.lives = lives;
        SceneManager.LoadScene(currentLevel);

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];

        transform.position = position;
        Debug.Log("gold: " + soul);
        Debug.Log("current_level: " + currentLevel);
    }

}
