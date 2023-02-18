using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFlyingEnemy : BasicEnemy
{
    protected bool AbilityUsed = false;
    protected ushort SkipNodes = 0;
    
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        if (_path.Count >= 4 && !AbilityUsed)
        {
            SkipWaypoints();
        }
        base.Update();
    }
    
    void SkipWaypoints()
    {
        if (fHealth <= 40f)
        {
            AbilityUsed = true;
            for (int i = 0; i < SkipNodes; i++)
            {
                _path.RemoveAt(i);
            }
        }
    }
}
