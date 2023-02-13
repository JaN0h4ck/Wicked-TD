using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour, Currency {

    [SerializeField]
    [Tooltip("Der Startbetrag des Spielers")]
    private int balance;

    [SerializeField]
    private string currencyName;

    [SerializeField]
    private TextMeshProUGUI balanceText;

    private void Start() {
        if(!string.IsNullOrEmpty(currencyName))
            balanceText.text = currencyName + ": " + balance;
    }

    int Currency.GetBalance() {
        return balance;
    }

    bool Currency.AddBalance(int amount) {
        if (balance + amount < 0)
            return false;
        else {
            balance += amount;
            return true;
        }
    }

    bool Currency.SubstractBalance(int amount) {
        if (balance - amount < 0)
            return false;
        else {
            balance -= amount;
            return true;
        }
    }

    string GetName() {
        return currencyName;
    }
}