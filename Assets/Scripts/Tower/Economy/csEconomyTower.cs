using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csEconomyTower : csTowerBaseScript
{
    #region Variables
    [SerializeField]
    private float fCurrencyGenerationSpeed;

    [SerializeField]
    private float fCurrencyGenerationAmount;
    #endregion

    #region currency

    private void Start()
    {
        Setup();
        StartCoroutine(GenerateCurrency());
        
    }

    private IEnumerator GenerateCurrency()
    {
        while(true)
        {
            Debug.Log("~Generated currency");
            yield return new WaitForSecondsRealtime(fCurrencyGenerationSpeed);
            csTowerManager.current.AddCurrencyToBalance(iFireMode,fCurrencyGenerationAmount);
        }
    }

   
    #endregion
}
