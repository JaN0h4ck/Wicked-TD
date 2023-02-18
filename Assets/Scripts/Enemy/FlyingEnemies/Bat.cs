using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : BasicFlyingEnemy
{
    new public void Start()
    {
        fHealth = 200f;
        movementSpeed = 1.0f;
        damageToNexus = 1f;
        SkipNodes = 1;
        
        base.Start();
    }

    new public void Update()
    {
        base.Update();
    }
}
