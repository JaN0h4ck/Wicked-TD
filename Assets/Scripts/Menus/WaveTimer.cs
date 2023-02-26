using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveTimer : MonoBehaviour {
    
    private TextMeshProUGUI m_text;
    private GameObject m_timer;

    private void Awake() {
        m_timer = GameObject.Find("Timer");
        m_text = m_timer.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        MapLogic.instance.onTimerStarted += h_WaveCountdown;
        m_timer.SetActive(false);
    }

    private void h_WaveCountdown() {
        StartCoroutine(DisplayWaveCountdown());
    }

    private IEnumerator DisplayWaveCountdown() {
        m_timer.SetActive(true);
        int time = (int)MapLogic.instance.getTimeBetweenWaves();
        while (time > 0) {
            m_text.text = "Next Wave in: " + time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }
        m_timer.SetActive(false);
    }
}
