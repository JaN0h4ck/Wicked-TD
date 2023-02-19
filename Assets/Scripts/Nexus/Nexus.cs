using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : csEnemyHealth {
    public static Nexus instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        fHealth = m_maxHealth;
    }


    public event Action onHealthLost;

    public bool alive = true;

    [SerializeField]
    private float m_maxHealth = 10;

    public float maxHealth {
        get { return m_maxHealth; }
    }

    protected override void CheckForDeath() 
    {
        if (fHealth <= 0)
        {
            alive = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        TransitionToGameOver();
    }

    public float getHealth()
    {
        return fHealth;
    }

    public override void LooseHealth(float fDamage) {
        base.LooseHealth(fDamage);
        onHealthLost?.Invoke();
    }

    
    private void TransitionToGameOver()
    {
        //TODO Dillon will game over screen implementieren
    }
    
}
