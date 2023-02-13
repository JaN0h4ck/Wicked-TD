using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Money {
    public interface Currency {
        int GetBalance();
        void AddBalance(int amount);
        void SubstractBalance(int amount);
    }

    public class MoneyPast : MonoBehaviour, Currency {
        
        private int balance;
        
        int Currency.GetBalance() {
            return balance;
        }
        void Currency.AddBalance(int amount) {
            balance += amount;
        }
        void Currency.SubstractBalance(int amount) {
            balance -= amount; 
        }
    }
    
    public class MoneyPresent : MonoBehaviour, Currency {
        
        private int balance;

        int Currency.GetBalance() {
            return balance;
        }
        void Currency.AddBalance(int amount) {
            balance += amount;
        }
        void Currency.SubstractBalance(int amount) {
            balance -= amount;
        }
    }

    public class MoneyFuture : MonoBehaviour, Currency {
        private int balance;

        int Currency.GetBalance() {
            return balance;
        }
        void Currency.AddBalance(int amount) {
            balance += amount;
        }
        void Currency.SubstractBalance(int amount) {
            balance -= amount;
        }
    }
}