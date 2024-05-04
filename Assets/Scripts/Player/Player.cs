using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int currentLevel;
    public int lives = GlobalVariables.lives;
    public int damage = GlobalVariables.damage;
    public int soul = GlobalVariables.soul;
    int helmetPrice = 100;
    int chestPlatePrice = 100;
    int bootsPrice = 100;

    //helmet purchase
    public bool isShopOpen = GlobalVariables.isShopOpen;
    public Button helmetButton;
    

    public void SavePlayer()
    {
        soul = GlobalVariables.soul;
        lives = GlobalVariables.lives;
        damage = GlobalVariables.damage;
        isShopOpen = GlobalVariables.isShopOpen;
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
        isShopOpen = data.isShopOpen;
        GlobalVariables.soul = soul;
        GlobalVariables.lives = lives;      
        GlobalVariables.damage = damage;
        GlobalVariables.isShopOpen = isShopOpen;
        SceneManager.LoadScene(currentLevel);

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];

        transform.position = position;
        Debug.Log("souls: " + soul);
        Debug.Log("current_level: " + currentLevel);
    }

    public void EquipItem()
    {
        if(helmetButton.tag == "Helmet")
        {
            if(GlobalVariables.soul >= helmetPrice)
            {
                GlobalVariables.damage += 10;
                GlobalVariables.soul -= helmetPrice;
                Debug.Log("helmet purchased");
                Debug.Log("player attack: " + GlobalVariables.damage);
                helmetButton.interactable = false;
            }           
            else
            {
                Debug.Log("Dont have enought soul to puchase a helmet");
            }
        }
        

        else if(gameObject.tag == "Chest Plate")
        {
            if (GlobalVariables.soul >= chestPlatePrice)
            {
                damage += 5;
                GlobalVariables.soul -= chestPlatePrice;
                GlobalVariables.lives += 2;
                Debug.Log("chest plate purchased");
            }
            else
            {
                Debug.Log("Dont have enought soul to puchase a chest plate");
            }

        }
        

        else if(gameObject.tag == "Boots")
        {
            if(GlobalVariables.soul >= bootsPrice)
            {
                GlobalVariables.soul -= bootsPrice;
                //ugorjon magasabbra ?
                Debug.Log("Boots purchased");
            }
            else
            {
                Debug.Log("Dont have enought soul to puchase");
            }
        }        
    }

}
