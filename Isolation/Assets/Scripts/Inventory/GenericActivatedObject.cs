using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericActivatedObject : MonoBehaviour, IActivatable 
{
    [SerializeField]
    private string nameText = "Generic Activated Object";

    public string NameText
    {
        get
        {
            return nameText;
        }
    }

    public void DoActivate()
    {
        Debug.Log(this.gameObject.name + " was activated.");
    }
}