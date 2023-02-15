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
   
    private enum CurrencyEnum
    {
        Gold,
        C6,
        Neoplasma
    }
    [SerializeField]
    [Tooltip("This sets the currency with which the tower will be payed")]
    private CurrencyEnum eBuildCurrency;

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
        Invoke("WeaponSetup", 0.01f);
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
    private void AddCurrency(int iIndex,int iAmount)
    {
        iaStoredCurrencies[iIndex]+= iAmount;
    }

    private void PullCurrency()
    {
        AddCurrency(iFireMode,WeaponManager.GetStoredCurrency());
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
            csTowerManager.current.AddCurrencyToBalance(i,iaStoredCurrencies[i]);
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


    /// <summary>
    /// Returns the sprite of the current ammunition
    /// </summary>
    /// <returns></returns>
    public Sprite GetCurrentAmmunitionVisuals()
    {

        return WeaponManager.GetBullet().GetComponent<SpriteRenderer>().sprite;
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

    #endregion
}
