using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTowerBaseScript : MonoBehaviour
{
    #region Variables
    
    [Tooltip("ThisContains the 3 Stored currencies and how much a tower stored of each currency")]
    private int[] iaStoredCurrencies = new int[3];

    private csWeapon WeaponManager;

    private csTowerManager TowerManager;

    [SerializeField]
    private float fBuildCost;

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
}
