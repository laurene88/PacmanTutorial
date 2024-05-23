using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostBehaviour
{
    // TODO THIS DOESNT WORK
    //TODO issue is now that frightened is being auto called with a duration of 0 at start?
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    private bool eaten;


    public override void Enable(float duration) //when frightened should go blue, then flash 2nd half of duration
    {        
        Debug.Log(ghost.name + "is frightened for POWER PELLET DURATION"+duration);
        base.Enable(duration);

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;

        Invoke(nameof(Flash), duration/2.0f);
    }

    public override void Disable() //disable- we wrote for behaviours, not OnDisable
    {
        base.Disable();

        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }


    private void Flash()
    {
        Debug.Log("FLASH method for " + ghost.name);
        if (!eaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimatedSprite>().Restart(); //make sure starts at start of anim.
        }
    }

    private void Eaten()
    {
        Debug.Log(ghost.name + "is eaten");
           eaten = true;
        //we just jump home instead of walking back when scared.
        ghost.SetPosition(ghost.home.insideTransform.position);
        //ghost.home.Enable(duration);
            
        ghost.home.Enable(ghost.frightened.duration); //stay home while frightened
        
        body.enabled = false;
         eyes.enabled = true;
         blue.enabled = false;
        white.enabled = false;
    }


    private void OnEnable()
    {
        blue.GetComponent<AnimatedSprite>().Restart();
        ghost.movement.speedMultiplier = 0.5f; //when frightened, slows and runs away
 
        eaten = false;
    }

    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1f;
        eaten = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled)
                Eaten();
        }
    }


}
