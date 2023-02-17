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

    private float fModifiedFireSpeed=0;

    [SerializeField]
    private float fShootRange;

    [SerializeField]
    private float fBulletSpeed;

    [SerializeField]
    [Tooltip("Sets how many enemies a toer can target ith one shot")]
    private int iTargetAmount;

    [SerializeField]
    [Tooltip("This value will be added to the wit time after each shot/earned currency. This variable is set for each tower individualy beacuse otherwise fast shooting towers would be completly busted compared to slow shoting ones")]
    public float fFireSpeedDeacrease;

    [SerializeField]
    [Tooltip("Put in here the bullets the tower can use. The tower will start shooting with the build at index =0")]
    private GameObject[] gaPrefabBullets;

    [SerializeField]
    [Tooltip("Set this Layermask to enemy so the toer can detect the enemies by using a overlapp box")]
    private LayerMask lmEnemy;

    [SerializeField]
    private GameObject gRangeIndicatorPrefab;

    [SerializeField]
    private int iCurrencyGainOnShot;

    [SerializeField]
    private Transform tsFirePoint;
    
    private int iStoredCurrency;
    

    private List<Transform> tslTargets;

    private GameObject gIndicatorEmpty;

    private int iBulletMode =0;

    private Vector3 v3LastEnemyPos;

    private csTowerManager TowerManager;

    protected Animator amAnimator;
    //index: 0 = idle, 1 = attack
    protected List<AnimationClip> aclAnimations;
    private int iAnimationTimeProgresion =0;

    private bool bLookDirectiom = false;
    private bool bCompareLookDirection = false;


    #endregion

    #region Setup
    private void Start()
    {
        tslTargets = new List<Transform>();
        gIndicatorEmpty = GameObject.Find("IndicatorEmpty");
        TowerManager = csTowerManager.current;
        aclAnimations = new List<AnimationClip>();
        if (gameObject.GetComponent<Animator>() != null)
        {
            amAnimator = gameObject.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("(csTowerBaseScript): Warning: Animator is not setup is this intentionall?");
        }
        GetAnimationClips();
    }
    #endregion

    #region Functions
    public IEnumerator ShootCooldown()
    {
        while (true)
        {
            tslTargets.Clear();
            while (IsEnemyInRange()==false)
            {
                GetNearestEnemy();
                yield return new WaitForSecondsRealtime(0.1f);
            }
            PlayAttackAnimation();
            Shoot();
            
            fFireSpeed += fFireSpeedDeacrease;
            Debug.LogWarning("%Waiting for " + fFireSpeed + fModifiedFireSpeed);
            yield return new WaitForSecondsRealtime(fFireSpeed+fModifiedFireSpeed);
        }
    }

    /// <summary>
    /// Searches for the nearest enemy and stores it in target
    /// </summary>
    private void GetNearestEnemy()
    {
        tslTargets.Clear();
        for (int i = iTargetAmount; i > 0; i--)
        {

            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, (transform.localScale / 2) * fShootRange, 0.0f, lmEnemy);
            Transform tsNearetEnemy = null;
            float fLowestDitance = 1000f;

            foreach (Collider2D cd in hitColliders)
            {

                Transform tsCurrent = cd.transform;
                if (tslTargets.Contains(tsCurrent) == false)
                {
                    float fCompare = Vector3.Distance(this.transform.position, tsCurrent.position);

                    if (fLowestDitance > fCompare)
                    {
                        fLowestDitance = fCompare;
                        tsNearetEnemy = tsCurrent;
                    }
                }
            }
            if (tsNearetEnemy != null&&tslTargets.Contains(tsNearetEnemy)==false)
            {
                Debug.Log("(Weapon): Found nearest Enemy:" + tsNearetEnemy.name);
                tslTargets.Add(tsNearetEnemy);
            }
            
            
        }
    }
    
    private bool IsEnemyInRange()
    {
        if(tslTargets.Count==0)
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
            SetupTargetsIfNecessary(tslTargets);
            SetupBullet();
        }
        else
        {
            if (TowerManager.bShowSiren == true)
            {
                EnableSiren();
            }
        }
    }

    /// <summary>
    /// Shows the Tower range to the user, you can change this in the Toermanager by unchecking bDisplayTowerRange
    /// </summary>
    public void DisplayTowerRangeToUser()
    {
        GameObject gTemp = Instantiate(gRangeIndicatorPrefab, this.transform.position, Quaternion.identity);
        gTemp.transform.localScale = new Vector2(0.115f*fShootRange,0.115f*fShootRange);
        gTemp.transform.SetParent(this.transform);
    }

    #region Money

    private bool TryBuyAmmunition()
    {
        Currency temp;
        if (Shop.Instance.currencyMap.TryGetValue("Gold",out temp))
        {
            if(temp.SubstractBalance(gaPrefabBullets[iBulletMode].GetComponent<csBullet>().GetAmmoCosts()))
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

    private void AddMoney()
    {
        iStoredCurrency += iCurrencyGainOnShot;
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
        foreach (Transform tsTarget in tslTargets)
        {
            SetLastEnemyPosition(tsTarget.position);
            LookAtEnemy(tsTarget);
            AddMoney();
            GameObject gTemp = Instantiate(gaPrefabBullets[iBulletMode], tsFirePoint.position, Quaternion.identity);

            csBullet Bullet = gTemp.GetComponent<csBullet>();

            Bullet.SetDamage(fDamage);
            Bullet.SetIndicatorEmpty(gIndicatorEmpty);
            Bullet.SetStartWeapon(this);

            if (Bullet != null)
            {
                Bullet.ShootAt(tsTarget, fBulletSpeed);
            }
            else
            {
                Debug.LogError("(csWepon): your bullet" + gTemp.name + " isnt setup correctly, the csBullet Script is missing");
            }
        }
    }

    public void SetupTargetsIfNecessary(List<Transform> tslTarget)
    {
        foreach (Transform tsTarget in tslTarget)
        {
            if (tsTarget.gameObject.GetComponent<csEnemyHealth>() == null)
            {
                tsTarget.gameObject.AddComponent<csEnemyHealth>();
            }
        }
    }

    public void ChangeBullet(int iBulletIndex)
    {
        if(iBulletIndex<gaPrefabBullets.Length)
        {
            iBulletMode = iBulletIndex;
        }
        else{
            iBulletMode = 0;
        }
    }

    public void ModifyDamage(float fFireSpeedModifier)
    {
        if(fFireSpeedModifier != fModifiedFireSpeed)
        {
            fModifiedFireSpeed = fFireSpeedModifier;
        }
    }

    public void ResetFireSpeedModifier()
    {
        fModifiedFireSpeed = 0;
    }
    #endregion

    #endregion

    #region Getter

    public float GetDamage()
    {
        return fDamage;
    }

    public GameObject[]  GetBullets()
    {
        return gaPrefabBullets;
    }

    public float GetRange()
    {
        return fShootRange;
    }

    public float GetFireSpeed()
    {
        return fFireSpeed;
    }

    public LayerMask GetEnemieLayer()
    {
        return lmEnemy;
    }

    public Vector3 GetLastEnemyPos()
    {
        return v3LastEnemyPos;
    }
    #endregion

    #region Setter

    public void SetLastEnemyPosition(Vector3 v3Pos)
    {
        v3LastEnemyPos = v3Pos;
    }
    #endregion

    #region Animation

    protected void GetAnimationClips()
    {
        if (amAnimator != null)
        {
            foreach (AnimationClip ac in amAnimator.runtimeAnimatorController.animationClips)
            {
                aclAnimations.Add(ac);
            }
        }
    }

    protected void PlayAttackAnimation()
    {
        if (aclAnimations.Count >= 2)
        {
            amAnimator.Play(aclAnimations[1+iAnimationTimeProgresion].name);
        }
    }

    protected void PlayIdleAnimation()
    {
        if (aclAnimations.Count != 0)
        {
            amAnimator.Play(aclAnimations[0 + iAnimationTimeProgresion].name);
        }
    }

    public void UpdateAnimationTimeProgressionToNextStage()
    {
        iAnimationTimeProgresion += 2;
    }

    private void LookAtEnemy(Transform tsTarget)
    {
        Debug.LogWarning("§Switching");
        if(tsTarget.position.x>=transform.position.x)
        {
            bLookDirectiom = false;
        }
        else
        {
            bLookDirectiom = true;
        }
        if(bLookDirectiom!=bCompareLookDirection)
        {
            bCompareLookDirection = bLookDirectiom;
            FlipObjectAlongX();
        }
    }


    private void FlipObjectAlongX()
    {
        transform.Rotate(0, 180, 0);
    }
    #endregion
}
