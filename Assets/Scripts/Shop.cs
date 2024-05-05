using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    bool canOpen = false;

    private void Start()
    {
        if (GlobalVariables.newLevel)
        {
            shopPanel.SetActive(true);
            canOpen = true;
        }
        else
        {
            shopPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canOpen)
        {
            ToggleShopPanel();
        }
    }

    void ToggleShopPanel()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        canOpen = false;
        GlobalVariables.isShopOpen = false;
    }
}
