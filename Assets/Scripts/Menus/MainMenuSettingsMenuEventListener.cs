using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSettingsMenuEventListener : MonoBehaviour {

    private CanvasGroup m_canvasGroup;

    private void Start() {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    public void EnableButtonsForSettingsMenu() {
        m_canvasGroup.interactable = true;
    }

    public void DisableButtonsForSettingsMenu() {
        m_canvasGroup.interactable = false;
    }
}
