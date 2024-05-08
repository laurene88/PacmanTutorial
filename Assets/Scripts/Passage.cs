using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //change the position of what is colliding with this trigger (pacman)
        Vector3 position = collision.transform.position;
        position.x = this.connection.position.x;
        position.y = this.connection.position.y;
        collision.transform.position  = position;
    }
}
