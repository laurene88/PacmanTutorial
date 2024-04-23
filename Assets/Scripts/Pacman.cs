using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    private CircleCollider2D myCollider;
    private void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();  
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        Debug.Log(collision.collider.name);
    }
}
