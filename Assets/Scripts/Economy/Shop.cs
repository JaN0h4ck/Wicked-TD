using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class Shop : Singleton<Shop> {
    
    private Currency[] m_currencies;

    public Dictionary<string, Currency> currencyMap;

    private CanvasGroup m_shopUI;

    //[SerializeField]
    //private string m_currencyHandlerName = "Currency Handler";

    private bool m_isUnlimitedMoneyEnabled = false;

    private void Start() {
        /*
        m_shopUI = GetComponent<CanvasGroup>();
        m_shopUI.alpha = 0;
        m_shopUI.blocksRaycasts = false;
        */
        //GameObject.Find("InputSystem").GetComponent<PlayerInput>().actions["ToggleShop"].performed += _ => ToggleShop();

        h_CurrencySetup();
    }

    private void h_CurrencySetup() {
        //m_currencies = GameObject.Find(m_currencyHandlerName).GetComponents<Currency>();
        m_currencies = GetComponents<Currency>();

        currencyMap = new Dictionary<string, Currency>();

        foreach (Currency currency in m_currencies) {
            if (currency is Money)
                currencyMap.Add(((Money)currency).GetName(), currency);
            else
                currencyMap.Add("Base Coin", currency);
        }

        // Nur für Debug-Zwecke
        
        foreach (KeyValuePair<string, Currency> entry in currencyMap) {
            Debug.Log(entry.Key + " " + entry.Value.GetBalance());
        }
    }

    [Obsolete]
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
    }

    public void CloseShop() {
        m_shopUI.alpha -= 1;
        m_shopUI.blocksRaycasts = false;
    }

    public void ToggleShop() {
        if(m_shopUI.alpha > 0)
            CloseShop();
        else
            OpenShop();
    }

    public void toggleUnlimitedMoney() {
        if (m_isUnlimitedMoneyEnabled) {
            m_isUnlimitedMoneyEnabled = false;
            foreach (Currency currency in m_currencies) {
                currency.disableUnlimitedMoney();
            }
        } else { 
            m_isUnlimitedMoneyEnabled = true;
            foreach (Currency currency in m_currencies) {
                currency.enableUnlimitedMoney();
            }
        }
    }
}
