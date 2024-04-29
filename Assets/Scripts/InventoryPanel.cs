using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public GameObject equipmentPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
