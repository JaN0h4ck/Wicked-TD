using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csEnemyHealth : MonoBehaviour
{
    #region Variables
    private float fHealth = 100;
    #endregion


    #region HealthUpdate

    public void LooseHealth(float fDamage)
    {
        fHealth -= fDamage;
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if(fHealth<=0)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
