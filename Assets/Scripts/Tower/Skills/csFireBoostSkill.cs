using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csFireBoostSkill : csSkillBaseScript
{
    #region Variables

    [SerializeField]
    private float fBoostFactor;

    private csWeapon WeaponManager;
    #endregion

    void Start()
    {
        Invoke("Setup", 0.1f);
    }

    private void Setup()
    {
        WeaponManager = transform.parent.gameObject.GetComponent<csWeapon>();
        BoostWeapon();
        Invoke("StopEffect", fDuration);
    }
    private void BoostWeapon()
    {
        WeaponManager.SkillModifyFireSpeed(fBoostFactor);
    }
   

    private void StopEffect()
    {
        WeaponManager.ResetSkillFireSpeedModifier();
        Destroy(this.gameObject);
    }
}
