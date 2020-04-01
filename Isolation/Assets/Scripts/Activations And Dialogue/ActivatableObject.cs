using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableObject : MonoBehaviour, IActivatable
{
    private DialogueTrigger dialogueTrigger;

    [SerializeField]
    private string nameText;

    [SerializeField]
    private string descriptionText;

    public string NameText
    {
        get
        {
            return nameText;
        }
    }

    public string DescriptionText { get { return descriptionText; } }

    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }
    public void DoActivate()
    {
        dialogueTrigger.TriggerDialogue();
    }
}
