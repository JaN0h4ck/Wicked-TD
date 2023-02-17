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

    //Buttons sollen während der Animation disabled sein, damit die Animation besser aussieht.
    public void DisableButtonsForSettingsMenu() {
        m_canvasGroup.interactable = false;
    }
}
