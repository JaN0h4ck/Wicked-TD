using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
<<<<<<< Updated upstream
public class csFireBoostSkill : csSkillBaseScript
=======
public class csFireBoostSkill 
>>>>>>> main
=======
public class csFireBoostSkill 
=======
public class csFireBoostSkill : csSkillBaseScript
>>>>>>> 86b3f9ded5ddc747cb7411bef91e653e3e7ac74e
>>>>>>> Stashed changes
{
    #region Variables

    [SerializeField]
    private float fBoostFactor;
<<<<<<< HEAD
<<<<<<< Updated upstream
=======
    #endregion
   
=======
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======
    #endregion
   
>>>>>>> main
=======
>>>>>>> 86b3f9ded5ddc747cb7411bef91e653e3e7ac74e
>>>>>>> Stashed changes
}
