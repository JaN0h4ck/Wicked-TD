using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTowerManager : MonoBehaviour
{
    #region Singleton
    public static csTowerManager current;

    private void Awake()
    {
        if(current==null)
        {
            current = this;
        }
    }
    #endregion

    #region Variables
    [SerializeField]
    public bool bShowTowerRange;
    #endregion
}
