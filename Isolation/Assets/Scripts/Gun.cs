using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20;

    private GameObject bulletInstance;

    public void Shoot()
    {
        bulletInstance = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);//spawn bullet at the spawn point
        SetBulletValues(bulletInstance);//set all bullet variables
    }

    private void SetBulletValues(GameObject bulletInstance)
    {
        Bullet bullet = bulletInstance.GetComponent<Bullet>();
        bullet.Speed = bulletSpeed;
    }
}
