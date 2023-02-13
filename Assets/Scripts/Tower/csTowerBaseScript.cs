using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTowerBaseScript : MonoBehaviour
{
    #region Variables
    
    [Tooltip("ThisContains the 3 Stored currencies and how much a tower stored of each currency")]
    private int[] iStoredCurrencies = new int[3];

    private csWeapon WeaponManager;

    private csTowerManager TowerManager;

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
    void Update()
    {

    }

    #region Shoot

    #endregion
}
