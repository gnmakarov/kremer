using System;
using UnityEngine;

public class rectangle: MonoBehaviour
{
    public static int NUMBER = 0;
    void OnCollisionEnter2D(Collision2D collision)
    {
        NUMBER = Int32.Parse(collision.collider.name);
    }
}
