using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour, IButtonListener
{
    [HideInInspector] private string Name;
    [SerializeField] private GameObject panel;

    private bool hidden = false;

    void Start()
    {
        //just hide without playing resume sound
        panel.gameObject.SetActive(false);
        hidden = true;
    }

    public void Execute()
    {
        TogglePause();
    }

    public void TogglePause()
    {
        if (hidden)//if not paused
        {
            if (Time.timeScale == 1)//pause
                Time.timeScale = 0;

            panel.gameObject.SetActive(true);
            hidden = false;
            Cursor.visible = true;
        }
        else
        {
            if (Time.timeScale == 0)
                Time.timeScale = 1;

            panel.gameObject.SetActive(false);
            hidden = true;
            Cursor.visible = false;
        }
    }

    public void MainMenu()
    {
        Debug.Log("MainMenu");
    }
}
