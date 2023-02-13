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
    private float fBulletSpeed;

    [SerializeField]
    private GameObject gPrefabBullet;

    [SerializeField]
    [Tooltip("Set this Layermask to enemy so the toer can detect the enemies by using a overlapp box")]
    private LayerMask lmEnemy;

    [SerializeField]
    private GameObject gRangeIndicatorPrefab;

    [SerializeField]
    private int iCurrencyGainOnShot;
    
    private int iStoredCurrency;
    

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

            Transform tsCurrent = cd.transform;
            float fCompare = Vector3.Distance(this.transform.position, tsCurrent.position);

            if (fLowestDitance> fCompare)
            {
                fLowestDitance = fCompare;
                tsNearetEnemy = tsCurrent;
            }
        }
        if (tsNearetEnemy != null)
        {
            Debug.Log("(Weapon): Found nearest Enemy:" + tsNearetEnemy.name);
        }
        tsTarget = tsNearetEnemy;
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
        if (TryBuyAmmunition())
        {
            TryDisableSiren();
            iStoredCurrency+=iCurrencyGainOnShot;
            Debug.Log("§ " + iStoredCurrency);
            SetupBullet();
        }
        else
        {
            EnableSiren();
        }
    }

    /// <summary>
    /// Shows the Tower range to the user, you can change this in the Toermanager by unchecking bDisplayTowerRange
    /// </summary>
    public void DisplayTowerRangeToUser()
    {
        GameObject gTemp = Instantiate(gRangeIndicatorPrefab, this.transform.position, Quaternion.identity);
        gTemp.transform.localScale = new Vector2(0.115f*fShootRange,0.115f*fShootRange);
    }

    #region Money

    private bool TryBuyAmmunition()
    {
        Currency temp;
        if (Shop.Instance.currencyMap.TryGetValue("Gold",out temp))
        {
            if(temp.SubstractBalance(gPrefabBullet.GetComponent<csBullet>().GetAmmoCosts()))
            {
                return true;
            }
        }
        else
        {
            Debug.LogError("Currency not found");
            
        }
        return false;
    }

    /// <summary>
    /// Displays a siren under the tower
    /// This shows the player that the tower cant buy ammo anymore
    /// </summary>
    private void EnableSiren()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    /// <summary>
    /// Deactivates the out of money siren, this check wether the siren is activ or not before disabling it
    /// </summary>
    private void TryDisableSiren()
    {
        GameObject temp = this.transform.GetChild(0).gameObject;
        if(temp.activeSelf==true)
        {
            temp.SetActive(false);
        }
    }

    public int GetStoredCurrency()
    {
        return iStoredCurrency;
    }
    public void ResetStoredCurrency()
    {
        iStoredCurrency = 0;
    }
    #endregion

    #region Bullet
    private void SetupBullet()
    {
        GameObject gTemp = Instantiate(gPrefabBullet, this.transform.position, Quaternion.identity);

        csBullet Bullet = gTemp.GetComponent<csBullet>();
        if (Bullet != null)
        {
            Bullet.ShootAt(tsTarget, fBulletSpeed);
        }
        else
        {
            Debug.LogError("(csWepon): your bullet" + gTemp.name + " isnt setup correctly, the csBullet Script i missing");
        }
        
    }
    #endregion

    #endregion
}
