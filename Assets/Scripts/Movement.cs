using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8f;
    public float speedMultiplier = 1f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public new Rigidbody2D rigidbody { get; private set; } //keyword new hides property with same name
    public Vector2 direction;
    public Vector2 nextDirection; //can kind of stack direction plans
    public Vector3 startingPosition { get; private set; } // this way we can reset our position on game reset.
       

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    public void Start()
    {
        ResetState();
    }
    
    public void ResetState()
    {
        speedMultiplier = 1;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        rigidbody.isKinematic = false;
        enabled = true;
    }

    private void FixedUpdate() //for physics stuff.
    {
        //note fixed delta time used.
        //physics usually do in fixed update or else its inconsistent across computer
        Vector2 position = this.rigidbody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;
        this.rigidbody.position = (position + translation);
        //this.rigidbody.MovePosition(position + translation); //THIS DIDN'T WORK??? //CHECK

        //Debug Logs of whole process:
            //Debug.Log("position:" + position);
            // Debug.Log("direction:" + direction + "speed:" + speed + "multiplier:" + speedMultiplier);
            //Debug.Log("translation=" + translation);
            // Debug.Log("added together:"+(position + translation));
    }


    private void Update()
    {
        if (nextDirection != Vector2.zero) //each frame, try to move in the next direction
        {
            SetDirection(nextDirection);
        }
    }


    public void SetDirection(Vector2 direction, bool forced = false)
    {
        Debug.Log("SetDirection");
        if (forced || !Occupied(direction))
        {
            this.direction = direction; // if can move, move & reset next direction queue
            nextDirection = Vector2.zero; //clear queued direction
            Debug.Log("setdirection & can move");
        } else
        {
            nextDirection = direction;// if wall in the way, line up the direction as next.
            Debug.Log("sset direction, cannot move.");
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null; //if you hit something it wont be null. 
    }

}
