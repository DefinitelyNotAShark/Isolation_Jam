using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10;

    [HideInInspector] public AudioManager audio;

    [SerializeField] private DebugScreen debugScreen;

    private void Start()
    {
        audio = AudioManager.instance;
    }

    public void Move(float x, float y)
    {
        transform.Translate(dir(x, y) * speed * Time.deltaTime);
        debugScreen.DisplayText(dir(x, y).ToString());
    }

    public void GunAttack()
    {
        audio.PlaySound("Shoot");
    }

    public void SwordAttack()
    {
        audio.PlaySound("Slash");
    }

    #region direction check
    private Vector3 dir(float x, float y)
    {
        if (x == 0 && y == 0)//first check for no input
            return new Vector3(0, 0, 0);
        if (y == 0)//if only pressing horizontal
        {
            if (x < 0)
                return new Vector3(-1, 0, 0);//left      

            if(x > 0)
                return new Vector3(1, 0, 0);//right
        }
        else if (x == 0)//if only pressing vertical
        {
            if (y < 0)
                return new Vector3(0, 0, -1);//down
            if(y > 0)
                return new Vector3(0, 0, 1);//up
        }
        else if (y < 0)//downs
        {
            if (x < 0)
                return new Vector3(-1, 0, -1);//down left
            if (x > 0)
                return new Vector3(1, 0, -1);//down right
        }
        else//ups
        {
            if (x < 0)
                return new Vector3(-1, 0, 1);//up left
            if (x > 0)
                return new Vector3(1, 0, 1);//up right
        }
        return new Vector3(0, 0, 0);//default no movement
    }
    #endregion
}
