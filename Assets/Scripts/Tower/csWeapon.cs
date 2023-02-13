using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csWeapon : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float fDamage;

    [SerializeField]
    [Tooltip("This sets the cooldown a tower has to wait after a hot before a ne shot (in seconds)")]
    private float fFireSpeed;

    [SerializeField]
    private float fShootRange;

    [SerializeField]
    private GameObject gPrefabBullet;

    [SerializeField]
    [Tooltip("Set this Layermask to enemy so the toer can detect the enemies by using a overlapp box")]
    private LayerMask lmEnemy;

    private Transform tsTarget;
    #endregion


    #region Functions
    public IEnumerator ShootCooldown()
    {
        while (true)
        {
            GetNearestEnemy();
            if (IsEnemyInRange())
            {
                Shoot();
            }
            yield return new WaitForSecondsRealtime(fFireSpeed);
        }
    }

    /// <summary>
    /// Searches for the nearest enemy and stores it in target
    /// </summary>
    private void GetNearestEnemy()
    {
        //transform.localScale / 3 draws the box exactly around the object
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, (transform.localScale / 2) * fShootRange, 0.0f, lmEnemy);
        Transform tsNearetEnemy=null;
        float fLowestDitance =1000f;

        foreach(Collider2D cd in hitColliders)
        {
            Debug.Log("(Weapon): Found Enemies:" + cd.gameObject.name);

            Transform tsCurrent = cd.transform;
            float fCompare = Vector3.Distance(this.transform.position, tsCurrent.position);

            if (fLowestDitance> fCompare)
            {
                fLowestDitance = fCompare;
                tsNearetEnemy = tsCurrent;
            }
        }
    }
    
    private bool IsEnemyInRange()
    {
        if(tsTarget==null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Shoot()
    {
        Debug.Log("(Tower): Shoot " + gameObject.name);
        SetupBullet();
    }

    #region Bullet
    private void SetupBullet()
    {
        GameObject gTemp = Instantiate(gPrefabBullet, this.transform.position, Quaternion.identity);
        
    }
    #endregion

    #endregion
}
