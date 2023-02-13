using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csExplosionTower : csTowerBaseScript
{

    /// <summary>
    /// In this case The on destroy Command is used to summon a explosion effect
    /// </summary>
    private void OnDestroy()
    {
        Debug.Log("(ExplosionTower): exploding " + gameObject.name);
    }
}
