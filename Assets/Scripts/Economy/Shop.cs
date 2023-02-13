using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour {

    private Currency[] currencies;

    private Dictionary<string, Currency> currencyMap;

    private void Start() {
        currencies = GameObject.Find("Currency Handler").GetComponents<Currency>();

        currencyMap = new Dictionary<string, Currency>();

        foreach (Currency currency in currencies) {
            if (currency is Money) 
                currencyMap.Add(((Money)currency).GetName(), currency);
            else
                currencyMap.Add("Base Coin", currency);
        }

        /* Nur f�r Debug-Zwecke
        foreach (KeyValuePair<string, Currency> entry in currencyMap) {
            Debug.Log(entry.Key + " " + entry.Value.GetBalance());
        }*/
    }

    public void BuyTower() {
        //Hier noch irgendwie getter rein, der ben�tigten Coin Type vom Tower holt
        Currency baseCoin;
        if(currencyMap.TryGetValue("Base Coin", out baseCoin)) {
            if(baseCoin.SubstractBalance(10))
                Debug.Log("Tower bought");
            else
                Debug.Log("Not enough money");
        }
    }
}
