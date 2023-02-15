using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csExplosionEffectSwitchOff : MonoBehaviour
{
    /*
     * This just sets the explosion effect inactive after some sec
     */

    private void Start()
    {
        Invoke("SwitchOff", 3f);
    }

    private void SwitchOff()
    {
        Destroy(this.gameObject);
    }
}
