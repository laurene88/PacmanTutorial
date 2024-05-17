using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }


    //when hit a node, choose a random available direction to travel in
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>(); //if collide with a node

        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        { //&if this behaviour is enabled - as this will be called regardless
          // & is not frightened, can be both & dont want to do anything.
           // Debug.Log("node hit, I can go in " + node.availableDirections.Count + " directions");
            int index = Random.Range(0, node.availableDirections.Count);
            //dont want it always going back and forward, so check - if its hte opposite of current, dont pick it
            if (node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1)
            {
               // Debug.Log("chose the opposite of current so changing");
                index++; //just increase who cares. take care of wrap.
                if (index >= node.availableDirections.Count)
                {
                  //  Debug.Log("chose out of bounds so changing");
                    index = 0;
                }
            }
            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }

}
