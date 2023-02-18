using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : csEnemyHealth
{
    public bool alive = true;
    void Start()
    {
        fHealth = 10;
    }

    protected override void CheckForDeath() 
    {
        if (fHealth <= 0)
        {
            alive = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        TransitionToGameOver();
    }

    private void TransitionToGameOver()
    {
        //TODO Dillon will game over screen implementieren
    }
    
}
