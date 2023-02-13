using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Money {
    public interface Currency {
        int GetBalance();
        /// <summary>
        /// Addiert einen Betrag zum Kontostand
        /// </summary>
        /// <param name="amount"></param>
        /// <returns> "true", wenn der Kontostand danach > 0, ansonsten "false" </returns>
        bool AddBalance(int amount);
        /// <summary>
        /// Subtrahiert einen Betrag vom Kontostand
        /// </summary>
        /// <param name="amount"></param>
        /// <returns> "true", wenn der Kontostand danach > 0, ansonsten "false" </returns>
        bool SubstractBalance(int amount);
    }

    public class Money : MonoBehaviour, Currency {
        
        private int balance;
        
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
            if(balance - amount < 0)
                return false;
            else {
                balance -= amount;
                return true;
            }
        }
    }
}