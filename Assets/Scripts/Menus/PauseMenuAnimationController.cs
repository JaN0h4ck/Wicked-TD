using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;

public class PauseMenuAnimationController : MonoBehaviour {
    
    private Button[] buttons;
    private Animator m_pauseMenuAnimator;

    private void Awake() {
        buttons = GetComponentsInChildren<Button>();
        m_pauseMenuAnimator = GetComponent<Animator>();
    }

    //Buttons sollen während der Animation disabled sein, damit die Animation besser aussieht.
    public void DisableButtons() {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].interactable = false;
        }
    }

    
    public void EnableButtons() {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].interactable = true;
        }
        m_pauseMenuAnimator.enabled = false;
    }
}
