using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on tutorial:
// https://www.youtube.com/watch?v=TKt_VlMn_aA&ab_channel=Zigurous

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    [SerializeField]
    public int score { get; private set; }
    public int lives { get; private set; }
    public int ghostMultiplier { get; private set; } = 1;

    private void Start()
    {
        NewGame();
    }


    private void Update()
    {
        if (this.lives<=0 && Input.anyKeyDown) //restart after died
        {
            NewGame();
        }
    }


    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        //loop thru all of pellets & turn them back on.
        // this is why its a transform, so we can go thry all the children
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }



    private void ResetState() //without restarting pellets
    {
        ResetGhostMultipler();
        for (int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].gameObject.SetActive(true);
        }

        this.pacman.gameObject.SetActive(true);
    }


    private void SetScore(int score)
    {
        this.score = score;
    }


    private void SetLives(int lives)
    {
        this.lives = lives;
    }


    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(this.score + (ghost.points * ghostMultiplier));
        ghostMultiplier += 1;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false); //turn off immediately
        SetLives(this.lives -1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState),3f); //reseat ghosts & pacman, not pellets.
        }
        else
        {
            GameOver(); 
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false); //turn off so cant die in this changeover time. 
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        PelletEaten(pellet);
        CancelInvoke(); //so resets to longer duration if already eaten a pellet
        Invoke(nameof(ResetGhostMultipler), pellet.duration);
     
        //TODO change ghost state when power pellet is eaten
    }

    private void ResetGhostMultipler() 
    {
        ghostMultiplier = 1;
     }
 
    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }


}
