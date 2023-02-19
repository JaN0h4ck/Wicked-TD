using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : BasicFlyingEnemy
{
    
    new void Start()
    {
        movementSpeed = 2f;
        damageToNexus = 2f;
        SkipNodes = 0;
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
