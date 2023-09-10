using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private GameObject startWaveButton;

    private void Awake() {
        MapLogic.Instance.OnWaveStarted += OnWaveBegin;
        MapLogic.Instance.OnTimerInterrupt += OnWaveBegin;
        MapLogic.Instance.OnTimerStarted += OnWaveEnd;
        OnWaveBegin();
    }
    
    void OnWaveEnd() {
        startWaveButton.SetActive(true);
    }

    void OnWaveBegin() {
        startWaveButton.SetActive(false);
    }
}
