using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugScreen : MonoBehaviour, IButtonListener
{
    private TextMeshPro debugText;
    private GameObject panel, debugTextInstance;

    private void Start()
    {
        panel = gameObject;
    }

    void ShowPanel()
    {
        panel.SetActive(true);
    }
    void HidePanel()
    {
        panel.SetActive(false);
    }

    public void DisplayText(string text)
    {
        debugTextInstance = Instantiate(new GameObject(), this.gameObject.transform);
        debugTextInstance.AddComponent<TextMeshPro>().text = text;//add text component with the text of "TEXT"
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
