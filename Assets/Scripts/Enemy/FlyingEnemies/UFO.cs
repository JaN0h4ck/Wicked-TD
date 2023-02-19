using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : BasicFlyingEnemy
{
    
    new void Start()
    {
        movementSpeed = 1.5f;
        damageToNexus = 4f;
        SkipNodes = 3;
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }
}
