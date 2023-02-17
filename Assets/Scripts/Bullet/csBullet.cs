using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class csBullet : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private int iShootCost;

    private csTransformations2D Transformation;

  
    private ParticleSystem psTrailEffect;

    [SerializeField]
    private bool bAttachEffectToObject;

    private float fDamage;

    [SerializeField]
    private GameObject gDamageIndicatorPrefab;

    [SerializeField]
    private float fDamageModifier;

    [SerializeField]
    [Tooltip("This ill be multiplied ith the orginal firespeed value")]
    private float fFireSpeedModifier;

    private csWeapon WeaponManager;

    private GameObject gIndicatorEmpty;

    private Transform tsEnemy;


    #endregion

    #region Setup
    /// <summary>
    /// Define the object at which the bullet is shot
    /// </summary>
    public void ShootAt(Transform tsTarget, float fBulletSpeed) {
        Transformation = gameObject.GetComponent<csTransformations2D>();
        if (Transformation != null) {
            Transformation.MoveTowardsLoop(this.transform, tsTarget, fBulletSpeed);
            StartCoroutine(WaitTillTargetHit(tsTarget));
        } else {
            Debug.LogError("Your bullet is etup incorrectly. Its missing a csTransformation2D script - called by " + this.gameObject);
        }
        if(this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>()!=null)
        {
            psTrailEffect = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            if(bAttachEffectToObject==false)
            {
                this.transform.GetChild(0).SetParent(null);
            }
            psTrailEffect.Play();
        }
        else
        {
            Debug.LogWarning("Your bullet migth be setup incorrectly. Its missing a Trail Particle System - called by " + this.gameObject);
        }
        WeaponManager.ModifyDamage(fFireSpeedModifier);
    }

    public void SetStartWeapon(csWeapon Weapon)
    {
        WeaponManager = Weapon;
    }
    #endregion

    #region FlyBehaviour
    private IEnumerator WaitTillTargetHit(Transform tsTarget) {
        tsEnemy = tsTarget;
        bool isRunning = true;
        while(isRunning&&tsTarget!=null) {
            Transformation.LookAt2D(this.transform, tsTarget);
            yield return new WaitForSecondsRealtime(0.05f);
            
            if(WasTargetHit(tsTarget)) {
                isRunning = false;
            }
        }

        if (tsTarget != null)
        {
            DoDamageToTarget();
        }
        Transformation.StopMoveTowardsLoop(this.transform);
        
        Destroy(this.gameObject);
    }

    private bool WasTargetHit(Transform tsTarget) {
        if (tsTarget != null)
        {
            if (tsTarget.position == this.transform.position)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Destroy(this.gameObject);
            return false;
        }
    }
    #endregion

    #region Damage

    public  void SetDamage(float fBulletDamage)
    {
        fDamage = fBulletDamage;
    }



    private void DoDamageToTarget()
    {
        if (tsEnemy.gameObject.GetComponent<csEnemyHealth>() != null)
        {
            tsEnemy.gameObject.GetComponent<csEnemyHealth>().LooseHealth(fDamage+fDamageModifier);
        }
        csDamageManager.current.ShowDamageAt(this.transform, fDamage + fDamageModifier);
    }

  

    #endregion
    #region Utility
    public int GetAmmoCosts()
    {
        return iShootCost;
    }

    public void SetIndicatorEmpty(GameObject gDamageIndicatorEmpty)
    {
        gIndicatorEmpty = gDamageIndicatorEmpty;
    }
    private void OnDestroy()
    {
        WeaponManager.ResetFireSpeedModifier();
    }
    #endregion
}
