using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuItem : MonoBehaviour 
{
    private InventoryMenu inventoryMenu;

    public InventoryObject InventoryObjectRepresented { get; set; }

    void Start () 
	{
        inventoryMenu = FindObjectOfType<InventoryMenu>();
	}

    public void OnValueChanged()
    {
        // Update the description area text!
        inventoryMenu.UpdateDescriptionAreaText(InventoryObjectRepresented.DescriptionText);
    }
}