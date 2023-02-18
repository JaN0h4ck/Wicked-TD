using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csEnemyHealth : MonoBehaviour
{
    #region Variables
    protected float fHealth = 100;
    #endregion


    #region HealthUpdate

    public virtual void LooseHealth(float fDamage)
    {
        fHealth -= fDamage;
        CheckForDeath();
    }

    protected virtual void CheckForDeath()
    {
        if(fHealth<=0)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
