using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugScreen : MonoBehaviour, IButtonListener
{
    private GameObject panel;
    private TextMeshProUGUI Text;

    private void Start()
    {
        panel = gameObject;
        Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void DisplayText(string text)
    {
        Text.text = text;
    }

    void ShowPanel()
    {
        panel.SetActive(true);
    }
    void HidePanel()
    {
        panel.SetActive(false);
    }

    /// <summary>
    /// Called by the buttonListener when the button designated by the input handler is pressed 
    /// </summary>
    public void Execute()
    {
        if (!panel.activeSelf)
            ShowPanel();
        else
            HidePanel();
    }
}