using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseCurrency : MonoBehaviour, Currency {

    [SerializeField]
    [Tooltip("Der Startbetrag für die Basiswährung")]
    private int m_balance;

    [SerializeField]
    [Tooltip("Die Anzahl der Sekunden, die vergehen, bevor der Spieler eine Basiswährung erhält")]
    private float m_scorePerSecond = 1.0f;

    [SerializeField]
    private TextMeshProUGUI m_balanceText;

    private float m_increaseTreshold = 0;

    void Start() {
        StartCoroutine(IncrementBaseCurrencyOverTime());
        m_balanceText.text = "Base Coin: " + m_balance;
    }

    private IEnumerator IncrementBaseCurrencyOverTime() {
        while (true) {
            m_increaseTreshold += m_scorePerSecond * Time.deltaTime;
            if (m_increaseTreshold >= 1) {
                m_balance += (int)m_increaseTreshold;
                m_balanceText.text = "Base Coin: " + m_balance;
                m_increaseTreshold -= m_increaseTreshold;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    int Currency.GetBalance() {
        return m_balance;
    }

    bool Currency.AddBalance(int amount) {
        if (m_balance + amount < 0)
            return false;
        else {
            m_balance += amount;
            return true;
        }
    }

    bool Currency.SubstractBalance(int amount) {
        if (m_balance - amount < 0)
            return false;
        else {
            m_balance -= amount;
            return true;
        }
    }
}
