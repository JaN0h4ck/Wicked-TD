using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{
    private uint currentWave = 0;
    private TextMeshProUGUI waveCounter;

    private void Awake() {
        MapLogic.Instance.OnWaveStarted += OnWaveStart;
        waveCounter = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnWaveStart() {
        currentWave++;
        UpdateText();
    }

    private void UpdateText() {
        waveCounter.text = "Wave " + currentWave.ToString() + " / 60";
    }
    // Start is called before the first frame update
    void Start() {
        UpdateText();
    }
}
