using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private void Start()
    {

    }

    public void Move(float x, float y)
    {
       transform.Translate(new Vector3(x * speed * Time.deltaTime, y * speed * Time.deltaTime));
    }

    public void Attack()
    {

    }
}
