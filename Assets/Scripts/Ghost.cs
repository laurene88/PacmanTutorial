using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }

    public GhostBehaviour initialBehaviour; //not private cause well set in editor
   public Transform target; //the object we'll be chasing/running from (pacma
    public int points = 200;

    public void Awake() //set all the references so that they can all reference each other via this.
    {
        movement = GetComponent<Movement>();
        home = GetComponent<GhostHome>();
        scatter = GetComponent<GhostScatter>();
        chase = GetComponent<GhostChase>();
        frightened = GetComponent<GhostFrightened>();
    }

    public void Start()
    {
        //ResetState(); - //this is called from game manager instead from NewGame
    }

    public void ResetState()
    {
        Debug.Log(this.name + "reseting state from Ghost/");
       // Debug.Log("reset ghost state - " + gameObject.name);
        gameObject.SetActive(true);
        movement.ResetState();

       //scatter.Enable(); //start in scatter.
        frightened.Disable();
        chase.Disable();

        if (home != initialBehaviour)
        {
            home.Disable();
        }

        if (scatter!= initialBehaviour)
        {
           scatter.Disable();
        }

        if (initialBehaviour != null)
        {
            initialBehaviour.Enable();
        }
    }


public void SetPosition(Vector3 position)
    {
        position.z = transform.position.z;
        transform.position = position;
    }

    //check for ghost collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
   

}
