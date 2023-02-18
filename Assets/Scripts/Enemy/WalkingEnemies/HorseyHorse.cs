using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseyHorse : WalkingEnemy
{
    new void Start()
    {
        movementSpeed = 0.3f;
        damageToNexus = 2.0f;
        
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }
}
