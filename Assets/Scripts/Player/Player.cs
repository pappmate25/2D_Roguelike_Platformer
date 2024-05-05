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
    public float critHitChance = GlobalVariables.critHitChance;
    public float critHitDMG = GlobalVariables.critHitDMG;
    public int soul = GlobalVariables.soul;
    int attackUpgradePrice = 100;
    int cHitChanceUpgradePrice = 200;
    int cHitDMGUpgradePrice = 200;

    //purchase
    public bool isShopOpen = GlobalVariables.isShopOpen;
    public Button attackUpgradeButton;
    public Button cHitChanceUpgradeButton;
    public Button cHitDMGUpgradeButton;
    

    public void SavePlayer()
    {
        soul = GlobalVariables.soul;
        lives = GlobalVariables.lives;
        damage = GlobalVariables.damage;
        critHitChance = GlobalVariables.critHitChance;
        critHitDMG = GlobalVariables.critHitDMG;
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
        critHitChance = data.critHitChance;
        critHitDMG = data.critHitDMG;
        soul = data.soul;
        isShopOpen = data.isShopOpen;
        GlobalVariables.soul = soul;
        GlobalVariables.lives = lives;      
        GlobalVariables.damage = damage;
        GlobalVariables.critHitChance = critHitChance;
        GlobalVariables.critHitDMG = critHitDMG;
        GlobalVariables.isShopOpen = isShopOpen;
        SceneManager.LoadScene(currentLevel);
        GlobalVariables.newLevel = false;

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];

        transform.position = position;
        Debug.Log("souls: " + soul);
        Debug.Log("current_level: " + currentLevel);
    }


    public void AttackPlus()
    {
        if (GlobalVariables.soul >= attackUpgradePrice)
        {
            GlobalVariables.damage += 10;
            GlobalVariables.soul -= attackUpgradePrice;
            Debug.Log("Attack Upgrade purchased");
            Debug.Log("player attack: " + GlobalVariables.damage);
        }
        else
        {
            Debug.Log("Dont have enought soul to puchase this upgrade");
        }       
    }

    public void critHitChancePlus()
    {       
        if (GlobalVariables.soul >= cHitChanceUpgradePrice)
        {
            GlobalVariables.critHitChance += 10;
            GlobalVariables.soul -= cHitChanceUpgradePrice;
            Debug.Log("Crit chance Upgrade purchased");
            Debug.Log("player critHitChance: " + GlobalVariables.critHitChance);
        }
        else
        {
            Debug.Log("Dont have enought soul to puchase this upgrade");
        }
    }
    public void critHitDMGPlus()
    {   
        if(GlobalVariables.soul >= cHitDMGUpgradePrice)
        {
            GlobalVariables.critHitDMG += 10;
            GlobalVariables.soul -= cHitChanceUpgradePrice;
            Debug.Log("Crit DMG Upgrade purchased");
            Debug.Log("player crithitdmg: " + GlobalVariables.critHitDMG);
        }
        else
        {
            Debug.Log("Dont have enought soul to puchase this upgrade");
        }  
    }

}
