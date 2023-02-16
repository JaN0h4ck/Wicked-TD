using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;

public class PauseMenuAnimationController : MonoBehaviour {
    // An array of buttons in the pause menu
    private Button[] buttons;

    private void Awake() {
        buttons = GetComponentsInChildren<Button>();
    }

    // A function to disable all buttons
    public void DisableButtons() {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].interactable = false;
        }
    }

    // A function to enable all buttons
    public void EnableButtons() {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].interactable = true;
        }
    }
}
