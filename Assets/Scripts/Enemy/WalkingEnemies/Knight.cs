using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : WalkingEnemy
{
    new void Start()
    {
        movementSpeed = 0.5f;
        damageToNexus = 0.5f;
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }
}
