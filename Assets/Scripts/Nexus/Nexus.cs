using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : csEnemyHealth
{
    // Start is called before the first frame update
    void Start()
    {
        fHealth = 10;
    }

    protected override void CheckForDeath() 
    {
        base.CheckForDeath();
        TransitionToGameOver();
    }

    private void TransitionToGameOver()
    {
        //TODO Dillon will game over screen implementieren
    }
    
}
