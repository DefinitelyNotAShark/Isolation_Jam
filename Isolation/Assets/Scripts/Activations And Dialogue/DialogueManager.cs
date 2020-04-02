using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public RectTransform dialogueBox;
    [SerializeField] private Vector2 visiblePos, offScreenPos;
    
    private Queue<string> sentences;

    [SerializeField] private Player characterController;
    [SerializeField] private ActivateLookedAtObjects activateObj;

    private bool isDialogueBoxVisible, isTalking;
    [SerializeField] private float typingSpeed=.02f,textBoxSpeed;
    [SerializeField]
    private string activateButton = "Interact";
    private AudioManager audioManager;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        characterController = characterController.GetComponent<Player>();
        activateObj = activateObj.GetComponent<ActivateLookedAtObjects>();
        sentences = new Queue<string>();
        EndDialogue();
    }
    private void Update()
    {
        UpdatePlayer();
    }
    public void StartDialogue(Dialogue dialogue)//Dialogue Trigger calls this function
    {
        isDialogueBoxVisible = true;
        OpenDialogue();
        Debug.Log("Starting dialogue with " + dialogue.name);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()//attach this event to the continue button
    {
        if (sentences.Count==0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        //dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(Type(sentence));
    }
    IEnumerator Type(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    private void EndDialogue()
    {
        Debug.Log("End of Dialogue");
        isDialogueBoxVisible = false;
        CloseDialogue();
    }
    private void UpdatePlayer()
    {
        if (isDialogueBoxVisible)
        {
            if (Input.GetButtonDown(activateButton))
            {
                DisplayNextSentence();
            }
        }

        characterController.enabled = !isDialogueBoxVisible;//This stops the player from moving during dialogue
        activateObj.enabled = characterController.enabled;
    }
    void OpenDialogue()
    {
        dialogueBox.DOAnchorPos(visiblePos, typingSpeed, true);
        Cursor.visible = true;
    }
    void CloseDialogue()
    {
        dialogueBox.DOAnchorPos(offScreenPos, typingSpeed, true);
        ///Cursor.visible = false;
    }
}
