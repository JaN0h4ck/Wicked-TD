using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTowerAbilities : MonoBehaviour
{
    #region Singleton
    public static csTowerAbilities current;
    #endregion
    #region Setup
    private void Awake()
    {
        if(current==null)
        {
            current = this;
        }
    }
    #endregion

    #region Variabless

    public enum Abilities{
        PlasmaExplosion,
        Thunder
    }

    #endregion
}
