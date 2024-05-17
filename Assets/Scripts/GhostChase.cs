using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehaviour
{

    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>(); //if collide with a node

        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            //loop through all nodes directions, then calculate if that gets us closer/farther to target (pacman)
            // go in direction that gets us closest to pacman.
            Vector2 direction = Vector2.zero;
            float minDistance = float .MaxValue; //set to max then initial will always be less.

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f); //new pos if we were to move in that direction
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude; //USE THIS NOT MAGNITUDE - WAY BETTER.
                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }
            this.ghost.movement.SetDirection(direction);
        }
    }


}
