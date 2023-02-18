using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : WalkingEnemy
{
    new void Start()
    {
        movementSpeed = 0.4f;
        damageToNexus = 0.5f;
        
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

}
