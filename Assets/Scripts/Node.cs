using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> availableDirections { get; private set; }
    public LayerMask obstacleLayer;

    private void Start()
    {
        this.availableDirections = new List<Vector2>();
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);

    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        //made these even smaller than movement ones
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one *0.5f, 0.0f, direction, 1f, obstacleLayer);
        if (hit.collider == null)
        {
            this.availableDirections.Add(direction);    
        }
    }


}
