using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : Singleton<Shop> {

    private Currency[] currencies;

    public Dictionary<string, Currency> currencyMap;

    private CanvasGroup shopUI;

    private void Start() {
        shopUI = GetComponent<CanvasGroup>();
        shopUI.alpha = 0;

        #region Currency
        currencies = GameObject.Find("Currency Handler").GetComponents<Currency>();

        currencyMap = new Dictionary<string, Currency>();

        foreach (Currency currency in currencies) {
            if (currency is Money) 
                currencyMap.Add(((Money)currency).GetName(), currency);
            else
                currencyMap.Add("Base Coin", currency);
        }

        /* Nur für Debug-Zwecke
        foreach (KeyValuePair<string, Currency> entry in currencyMap) {
            Debug.Log(entry.Key + " " + entry.Value.GetBalance());
        }*/
        #endregion
    }

    private void Update() {
        if(Input.GetKeyUp(KeyCode.S)) {
            if(shopUI.alpha > 0)
                CloseShop();
            else
                OpenShop();
        }
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
        shopUI.alpha += 1;
    }

    public void CloseShop() {
        shopUI.alpha -= 1;
    }

    public void ShopButton() {
        if(shopUI.alpha > 0)
            CloseShop();
        else
            OpenShop();
    }
}
