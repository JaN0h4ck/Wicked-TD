using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonEventListener : MonoBehaviour
{
    private MyButton m_button;

    public static event Action _onPauseMenuWasOpened;
    public static event Action _onPauseMenuWasClosed;

    private void Awake() {
        m_button = GetComponent<MyButton>();
        m_button.onDown.AddListener(OnButtonDown);
    }

    private void OnButtonDown() {
        if(PauseMenu.GameIsPaused)
            _onPauseMenuWasClosed?.Invoke();
        else
            _onPauseMenuWasOpened?.Invoke();
    }
}
