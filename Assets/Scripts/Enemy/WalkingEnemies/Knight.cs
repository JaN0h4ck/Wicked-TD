using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : WalkingEnemy
{
    void Start()
    {
        movementSpeed = 0.5f;
        damageToNexus = 0.5f;
        base.Start();
    }

    void Update()
    {
        base.Update();
    }
}
