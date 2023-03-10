using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTowerBaseScript : MonoBehaviour
{
    #region Variables
    
    [Tooltip("ThisContains the 3 Stored currencies and how much a tower stored of each currency")]
    private int[] iaStoredCurrencies = new int[3];

    protected csWeapon WeaponManager;

    private csTowerManager TowerManager;

    [SerializeField]
    private float fBuildCost;
   
    public enum CurrencyEnum
    {
        Gold,
        C6,
        Neoplasma
    }
    [SerializeField]
    [Tooltip("This sets the currency with which the tower will be payed")]
    private CurrencyEnum eBuildCurrency;

    [SerializeField]
    private float fDropDecrease;

    [SerializeField]
    [Tooltip("The Money that will be refunded form the buildcosts on destruction")]
    [Range(0,100)]
    private float fRefundPercentage;

    [SerializeField]
    [Tooltip("This sets which currency will be used to shooot, therefore the damagetype will be changed")]
    protected int iFireMode;

    [SerializeField]
    [Tooltip("The script spawnes currency on death, as an animation-indicator that the player gained money. How much currency will be spawned is randomed between the two given values")]
    private Vector2Int v2RandomSpawnvaluesMinMaxOnDeath;

    [SerializeField]
    [Tooltip("This is ued to drop multiple different Currencies, for the currency drop animtion. 0=Gold, 1= C6, 2=NeoPlasma")]
    private GameObject[] gCurrencyAnimationPrefabs;

    [SerializeField]
    [Tooltip("Put in here all Skillprefabs. The tower ill be able to use them by calling TriggerSKill(index)")]
    private GameObject[] gaSkillPrefabs;

    public string sTowerName;

    private int iGenerationMode;
    #endregion
    #region Setup
    private void Start()
    {
        Setup();
    }
    protected void Setup()
    {
        Debug.Log("(Tower): Running setup on " + gameObject.name);
        TowerManager = csTowerManager.current;
        SetGenerationMode();
        SetupExplosionTowerBugFix();
        Invoke("WeaponSetup", 0.01f);
    }

    private void SetupExplosionTowerBugFix()
    {
        if(gameObject.name == "Explosion"|| gameObject.name == "Explosion(Clone)")
        {
            Debug.LogWarning("ya");
            this.transform.position += new Vector3(0,0.5f,0.0f);
        }
    }
    protected void SetGenerationMode()
    {
        switch(eBuildCurrency)
        {
            case (CurrencyEnum.Gold):
                iGenerationMode = 0;
                break;
            case (CurrencyEnum.C6):
                iGenerationMode = 1;
                break;
            case (CurrencyEnum.Neoplasma):
                iGenerationMode = 2;
                break;
        }

    }
    private void WeaponSetup()
    {

        if (gameObject.GetComponent<csWeapon>() != null)
        {
            WeaponManager = gameObject.GetComponent<csWeapon>();
            WeaponManager.StartCoroutine(WeaponManager.ShootCooldown());
        }
        else
        {
            Debug.LogError("(Setup-Error): The tower " + gameObject.name + " is missing a csWWeaponScript!");
        }
        if (TowerManager.bShowTowerRange)
        {
            WeaponManager.DisplayTowerRangeToUser();
        }
        
    }

    #endregion


    #region Shoot
    public void SetFireMode(int iFireModeIndex)
    {
        PullCurrency();
        WeaponManager.ResetStoredCurrency();
        iFireMode = iFireModeIndex;
       
    }
    #endregion

    #region Currency
    private void AddCurrency(int iIndex,float fAmount)
    {
        iaStoredCurrencies[iIndex]+= (int)fAmount;
    }

    public void PullCurrency()
    {
        AddCurrency(iGenerationMode,WeaponManager.GetStoredCurrency());
        WeaponManager.ResetStoredCurrency();
    }

    private void OnDestroy()
    {
        DropMoneyOnDeath();
        CurrencyDropAnimation();
        
    }
    protected void DropMoneyOnDeath()
    {
        PullCurrency();
        for(int i = 2;i>=0;i--)
        {
            if(fDropDecrease==0)
            {
                fDropDecrease = 1;
            }
            csTowerManager.current.AddCurrencyToBalance(i,(int)((float)iaStoredCurrencies[i]/fDropDecrease));
            Debug.LogWarning("%"+ (int)iaStoredCurrencies[i]/fDropDecrease);
        }
        RefundBuildCosts();
    }
    
    protected void CurrencyDropAnimation()
    {
        for (int u = 2; u >= 0; u--)
        {
            for (int i = GetRandomNumber(); i > 0; i--)
            {
                if (iaStoredCurrencies[u] > 0)
                {
                    Instantiate(gCurrencyAnimationPrefabs[u], this.transform.position, Quaternion.identity);
                }
                else
                {
                    Debug.Log("%No currencies in store");
                }
            }
        }
    }

    private void RefundBuildCosts()
    {
        switch(eBuildCurrency)
        {
            case(CurrencyEnum.Gold):
                TowerManager.AddCurrencyToBalance(0, CalculateRefund(0));
                break;
            case (CurrencyEnum.C6):
                TowerManager.AddCurrencyToBalance(1, CalculateRefund(1));
                break;
            case (CurrencyEnum.Neoplasma):
                TowerManager.AddCurrencyToBalance(2, CalculateRefund(2));
                break;
        }
    }

    private int CalculateRefund(int iCurrencyIndex)
    {
        return (int)((fBuildCost/ 100) * fRefundPercentage);
    }
    #endregion

    #region Skills

    public void ActivateSkill(int iSkillIndex)
    {
        GameObject gTemp =Instantiate(gaSkillPrefabs[iSkillIndex], this.transform.position, Quaternion.identity);
        gTemp.transform.SetParent(this.transform);
    }


    #endregion

    #region utility
    /// <summary>
    /// Gets a random currency spawn amount
    /// This is only used for an animation
    /// </summary>
    /// <returns></returns>
    private int GetRandomNumber()
    {
        return Random.Range(v2RandomSpawnvaluesMinMaxOnDeath.x, v2RandomSpawnvaluesMinMaxOnDeath.y);
    }
    #endregion

    #region Getter

    public string GetTowerName()
    {
        return this.gameObject.name;
    }

    public float GetDamage()
    {
        CheckWeaponForNull();
        return WeaponManager.GetDamage();
    }

    public float GetRange()
    {
        CheckWeaponForNull();
        return WeaponManager.GetRange();
    }

    public CurrencyEnum GetCurrencyType()
    {
        return eBuildCurrency;
    }


    /// <summary>
    /// Returns the GameObjectsof the current ammunitions
    /// </summary>
    /// <returns></returns>
    public GameObject[] GetCurrentAmmunition()
    {
        return WeaponManager.GetBullets();
    }

    public GameObject[] GetCurrentSkills()
    {
        return gaSkillPrefabs;
    }

    /// <summary>
    /// Returns all stored currencies
    /// This will always be a 3 entries long array
    /// Index:
    /// 0 = Gold
    /// 1 = C6
    /// 2 = Neoplasma
    /// </summary>
    /// <returns></returns>
    public int[] GetPoints()
    {
        return iaStoredCurrencies;
    }

    public float GetBuildCosts()
    {
        return fBuildCost;
    }

    public float GetFireSpeed()
    {
        return WeaponManager.GetFireSpeed();
    }

    private void CheckWeaponForNull()
    {
        if(WeaponManager ==null)
        {
            WeaponManager = this.gameObject.GetComponent<csWeapon>();
        }
    }

    public csWeapon GetWeapon()
    {
        return WeaponManager;
    }


    public int GetWeaponMode()
    {
        return iGenerationMode;
    }
    #endregion

}
