using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    private CircleCollider2D myCollider;
    public Movement movement { get; private set; }


    private void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();  
        this.movement = GetComponent<Movement>();   
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            //Debug.Log("up");
            this.movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
           // Debug.Log("left");
            this.movement.SetDirection(Vector2.left);
        }
       else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
          //  Debug.Log("right");
            this.movement.SetDirection(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
          //  Debug.Log("down");
            this.movement.SetDirection(Vector2.down);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("pacman collided with "+collision.collider.name);
    }

    public void ResetState()
    {
      //  Debug.Log("resetpacman");
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    }
}
