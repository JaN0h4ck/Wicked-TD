using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : csEnemyHealth {
    public static Nexus instance;

    [SerializeField]
    private float m_maxHealth = 20;
    public float maxHealth {
        get { return m_maxHealth; }
    }

    public event Action onHealthLost;

    public event Action onGameOver;

    public bool alive = true;

    private SpriteRenderer _spriteRenderer;
    public Sprite[] sprites;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        fHealth = m_maxHealth;

        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        h_chooseSpriteForDisplay();
    }

    private void h_chooseSpriteForDisplay()
    {
        if (fHealth <= m_maxHealth/4)
        {
            _spriteRenderer.sprite = sprites[0];
        }
        else if (fHealth > m_maxHealth/4 && fHealth < m_maxHealth - m_maxHealth/4)
        {
            _spriteRenderer.sprite = sprites[1];
        }
        else
        {
            _spriteRenderer.sprite = sprites[2];
        }
    }


    protected override void CheckForDeath()
    {
        if (fHealth <= 0)
        {
            alive = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            TransitionToGameOver();
        }
    }

    public float getHealth()
    {
        return fHealth;
    }

    public override void LooseHealth(float fDamage) {
        base.LooseHealth(fDamage);
        h_chooseSpriteForDisplay();
        onHealthLost?.Invoke();
    }

    private void TransitionToGameOver()
    {
        onGameOver?.Invoke();
        //TODO Dillon will game over screen implementieren
    }

}
