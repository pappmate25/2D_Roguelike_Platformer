using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public GameObject equipmentPanel;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleEquipmentPanel();
        }
    }

    void ToggleEquipmentPanel()
    {
        equipmentPanel.SetActive(!equipmentPanel.activeSelf);
    }
}
