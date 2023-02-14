using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTowerManager : MonoBehaviour
{
    #region Singleton
    public static csTowerManager current;

    private void Awake()
    {
        if (current==null)
        {
            current = this;
        }
    }
    #endregion

    #region Variables
    [SerializeField]
    public bool bShowTowerRange;

    #endregion

    #region Money

    /// <summary>
    /// Adds any given money type to the currency balance
    /// </summary>
    /// <param name="iFireMode"></param>
    /// <param name="fCurrencyGenerationAmount"></param>
    public void AddCurrencyToBalance(int iFireMode, float fCurrencyGenerationAmount)
    {
        Currency temp = null;


        switch (iFireMode)
        {
            case (0):
                Shop.Instance.currencyMap.TryGetValue("Gold", out temp);
                break;
            case (1):
                Shop.Instance.currencyMap.TryGetValue("C6", out temp);
                break;
            case (2):
                Shop.Instance.currencyMap.TryGetValue("Neoplasma", out temp);
                break;
        }
        temp.AddBalance((int)fCurrencyGenerationAmount);
    }
    #endregion
}
