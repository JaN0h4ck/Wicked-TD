using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField]
    private Sprite[] m_healthBarStates;

    [SerializeField]
    private Image[] m_healthBar;

    private void Start() {
        Nexus.instance.onHealthLost += UpdateHealthBar;
        Debug.Log(m_healthBar.Length);
        UpdateHealthBar();
    }

    private void UpdateHealthBar() {
        Debug.Log("health: " + Nexus.instance.getHealth() + " maxhealth: " + Nexus.instance.maxHealth);
        double health = Nexus.instance.getHealth() / Nexus.instance.maxHealth;
        Debug.Log("health: " + health);
        double numberOfSegments = health * m_healthBar.Length;
        Debug.Log(numberOfSegments);
        double numberOfSegmentsBeforeDecimal = Math.Truncate(numberOfSegments);
        double numberOfSegmentsAfterDecimal = (numberOfSegments - numberOfSegmentsBeforeDecimal) * 10;
        Debug.Log(numberOfSegmentsAfterDecimal);

        for (int i = 0; i < numberOfSegmentsBeforeDecimal; i++) {
            m_healthBar[i].sprite = m_healthBarStates[2];
        }

        if(numberOfSegmentsAfterDecimal >= 0 && numberOfSegmentsAfterDecimal <= 5 && numberOfSegmentsBeforeDecimal < m_healthBar.Length) {
            m_healthBar[(int)numberOfSegmentsBeforeDecimal].sprite = m_healthBarStates[0];
        } else if(numberOfSegmentsAfterDecimal > 5) {
            m_healthBar[(int)numberOfSegmentsBeforeDecimal].sprite = m_healthBarStates[1];
        }

        int numberOfSegmentsRounded = (int)Math.Round(numberOfSegments, 0, MidpointRounding.ToEven);

        if (numberOfSegmentsRounded < m_healthBar.Length) {
            int emtpySegments = m_healthBar.Length - numberOfSegmentsRounded;
            for (int i = m_healthBar.Length -1; i >= numberOfSegmentsRounded; i--) {
                m_healthBar[i].sprite = m_healthBarStates[0];
            }
        }
    }
}
