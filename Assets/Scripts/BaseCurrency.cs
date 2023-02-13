using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseCurrency : MonoBehaviour, Currency {

    [SerializeField]
    [Tooltip("Der Startbetrag für die Basiswährung")]
    private int balance;

    [SerializeField]
    [Tooltip("Die Anzahl der Sekunden, die vergehen, bevor der Spieler eine Basiswährung erhält")]
    private float scorePerSecond = 1.0f;

    [SerializeField]
    private TextMeshProUGUI balanceText;

    private float increaseTreshold = 0;

    void Start() {
        StartCoroutine(IncrementBaseCurrencyOverTime());
        balanceText.text = "Base Coin: " + balance;
    }

    private IEnumerator IncrementBaseCurrencyOverTime() {
        while (true) {
            increaseTreshold += scorePerSecond * Time.deltaTime;
            if (increaseTreshold >= 1) {
                balance += (int)increaseTreshold;
                balanceText.text = "Base Coin: " + balance;
                increaseTreshold = 0;
            }
            yield return new WaitForEndOfFrame();
        }
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
}
