using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] private AudioManager audio;

    [SerializeField] private float speed = 3, dashSpeed = 6, dashDuration = .5f;
    [SerializeField] private DebugScreen debugScreen;

    [SerializeField] private GameObject bulletInstance, bulletSpawnPoint;

    private bool canDash = true;
    private Gun gun;

    private void Start()
    {
        audio = AudioManager.instance;
        gun = GetComponentInChildren<Gun>();
    }

    public void Move(float x, float y, bool canMove)
    {
        Vector3 moveDir = dir(x, y);
        transform.rotation = Quaternion.LookRotation(moveDir);

        if (canMove)
            transform.Translate(moveDir * speed * Time.deltaTime, Space.World);      
    }

    public void Dash()
    {
        if (canDash)
            StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        debugScreen.DisplayText("Dash!");
        canDash = false;

        for (float f = 0; f < dashDuration; f += Time.deltaTime)
        {
            transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        canDash = true;
        debugScreen.DisplayText("");
    }

    public void GunAttack()
    {
        audio.StopSound("Charge");
        audio.PlaySound("Shoot");
        gun.Shoot();
    }

    public void GunCharge()
    {
        audio.PlaySound("Charge");
        gun.Charge();
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
