using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;

    public void Move(float x, float y)
    {
       transform.Translate(new Vector3(x * speed * Time.deltaTime, 0, y * speed * Time.deltaTime));
    }

    public void Attack()
    {

    }
}
