using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject playerObject;
    Player player;

    private void Start()
    {
        player = playerObject.GetComponent<Player>();
    }
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GlobalVariables.lives = 3;
        GlobalVariables.isShopOpen = true;
        GlobalVariables.doneOneRun = false;
    }

    public void NewRun()
    {
        player.LoadPlayerStats();
        if (GlobalVariables.doneOneRun)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            GlobalVariables.lives = 3;
            GlobalVariables.isShopOpen = true;
        }
        else
        {
            NewGame();     
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
