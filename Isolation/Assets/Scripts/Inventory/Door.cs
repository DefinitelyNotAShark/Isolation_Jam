using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IActivatable
{
    [SerializeField]
    string nameText;
    [SerializeField]float yDirection=-8f, moveDuration=1f;

    [Tooltip("If you set a key, the door will be locked.")]
    [SerializeField]
    InventoryObject key;

    private bool isLocked, isOpen;
    private List<InventoryObject> playerInventory;

    public string NameText
    {
        get
        {
            string toReturn = nameText;

            if (isOpen)
                toReturn = ""; // So it doesn't look like you can open the door anymore.
            else if (isLocked && !HasKey)
                toReturn += " (LOCKED)";
            else if (isLocked && HasKey)
                toReturn += string.Format(" (use {0})", key.NameText);

            return toReturn;
        }
    }

    private bool HasKey
    {
        get
        {
            return playerInventory.Contains(key);
        }
    }

    public void DoActivate()
    {
        if (!isOpen)
        {
            if (!isLocked || HasKey)
            {
                //animator.SetBool("isDoorOpen", true);
                transform.DOMoveY(yDirection, moveDuration);
                isOpen = true;
            }
        }
    }

    // Use this for initialization
    void Start () 
	{
        playerInventory = FindObjectOfType<InventoryMenu>().PlayerInventory;
        isLocked = key != null;
	}
}