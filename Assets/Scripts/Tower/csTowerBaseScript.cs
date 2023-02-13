using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTowerBaseScript : MonoBehaviour
{
    #region Variables
    
    [Tooltip("ThisContains the 3 Stored currencies and how much a tower stored of each currency")]
    private int[] iStoredCurrencies = new int[3];

    private csWeapon WeaponManager;



    #endregion
    #region Setup
    void Start()
    {
        Debug.Log("(Tower): Running setup on " + gameObject.name);
        WeaponSetup();
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
    }

    #endregion
    void Update()
    {

    }

    #region Shoot

    #endregion
}
