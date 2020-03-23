using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [HideInInspector] public AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = AudioManager.instance;
    }

    public void StartEnter()
    {
        audio.PlaySound("ButtonHover");
    }

    public void StartPressed()
    {
        audio.PlaySound("ButtonPressed");
        SceneManager.LoadScene(1);
    }
}
