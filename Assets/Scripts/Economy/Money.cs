using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour, Currency {

    [SerializeField]
    [Tooltip("Der Startbetrag des Spielers")]
    private int m_balance;

    [SerializeField]
    private string m_currencyName;

    [SerializeField]
    private TextMeshProUGUI m_balanceText;

    private bool m_unlimitedMoney = false;

    private void Start() {
        if(!string.IsNullOrEmpty(m_currencyName))
            m_balanceText.text = m_currencyName + ": " + m_balance;
    }

    int Currency.GetBalance() {
        if (m_unlimitedMoney)
            return int.MaxValue;
        else
            return m_balance;
    }

    bool Currency.AddBalance(int amount) {
        if (m_unlimitedMoney)
            return true;
        else if (m_balance + amount < 0)
            return false;
        else {
            m_balance += amount;
            m_balanceText.text = m_currencyName + ": " + m_balance;
            return true;
        }
    }

    bool Currency.SubstractBalance(int amount) {
        if (m_unlimitedMoney)
            return true;
        else if (m_balance - amount < 0)
            return false;
        else {
            m_balance -= amount;
            m_balanceText.text = m_currencyName + ": " + m_balance;
            return true;
        }
    }

    public string GetName() {
        return m_currencyName;
    }

    void Currency.enableUnlimitedMoney() {
        m_unlimitedMoney = true;
        m_balanceText.text = m_currencyName + ": " + "Unlimited";
    }

    void Currency.disableUnlimitedMoney() {
        m_unlimitedMoney = false;
        m_balanceText.text = m_currencyName + ": " + m_balance;
    }
}