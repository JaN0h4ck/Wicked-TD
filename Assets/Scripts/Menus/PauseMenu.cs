using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    private GameObject pauseMenuUI;

    private InputAction m_pauseMenuToggleAction;

    private void Awake() {
        pauseMenuUI = GetComponentInChildren<CanvasGroup>(true).gameObject;

        m_pauseMenuToggleAction = GameObject.Find("InputSystem").GetComponent<PlayerInput>().actions["TogglePauseMenu"];
        m_pauseMenuToggleAction.performed += _ => TogglePauseMenu();
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void TogglePauseMenu() {
        if (GameIsPaused) {
            Resume();
        }
        else {
            Pause();
        }
    }

    public void gotoSettingsMenu() {
        Debug.Log("Settings Menu");
    }

    public void LoadMenu() {
        Debug.Log("Loading Menu...");
    }

    public void QuitGame() {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
