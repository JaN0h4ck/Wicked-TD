using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Shop : Singleton<Shop> {
    
    private Currency[] m_currencies;

    public Dictionary<string, Currency> currencyMap;

    private CanvasGroup m_shopUI;

    [SerializeField]
    private string m_currencyHandlerName = "Currency Handler";

    private PlayerInput m_playerinput;

    private InputAction m_closeShopInputAction;

    private void Start() {
        m_shopUI = GetComponent<CanvasGroup>();
        m_shopUI.alpha = 0;
        m_shopUI.blocksRaycasts = false;

        m_playerinput = GameObject.Find("InputSystem").GetComponent<PlayerInput>();

        m_playerinput.actions["ToggleShop"].performed += _ => ToggleShop();
        m_closeShopInputAction = m_playerinput.actions["CloseShop"];
        m_closeShopInputAction.performed += _ => CloseShop();
        m_closeShopInputAction.Disable();

        CurrencySetup();
    }

    private void CurrencySetup() {
        m_currencies = GameObject.Find(m_currencyHandlerName).GetComponents<Currency>();

        currencyMap = new Dictionary<string, Currency>();

        foreach (Currency currency in m_currencies) {
            if (currency is Money)
                currencyMap.Add(((Money)currency).GetName(), currency);
            else
                currencyMap.Add("Base Coin", currency);
        }

        // Nur für Debug-Zwecke
        /* 
        foreach (KeyValuePair<string, Currency> entry in currencyMap) {
            Debug.Log(entry.Key + " " + entry.Value.GetBalance());
        }*/
    }

    public void BuyTower(string CoinType) {
        Currency Coin;
        if(currencyMap.TryGetValue(CoinType, out Coin)) {
            if(Coin.SubstractBalance(10))
                Debug.Log("Tower bought");
            else
                Debug.Log("Not enough money");
        }
    }

    public void OpenShop() {
        m_shopUI.alpha += 1;
        m_shopUI.blocksRaycasts = true;
        m_closeShopInputAction.Enable();
    }

    public void CloseShop() {
        m_shopUI.alpha -= 1;
        m_shopUI.blocksRaycasts = false;
        m_closeShopInputAction.Disable();
    }

    public void ToggleShop() {
        if(m_shopUI.alpha > 0)
            CloseShop();
        else
            OpenShop();
    }
}
