using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    private GameObject m_pauseMenuUI;
    private CanvasGroup m_pauseMenuCanvasGroup;
    private Animator m_pauseMenuAnimator;

    private GameObject m_settingsMenuUI;

    private void Awake() {
        CanvasGroup[] tempCanvasGroup = GetComponentsInChildren<CanvasGroup>(true);
        GameObject[] tempGameObjects = new GameObject[tempCanvasGroup.Length];

        for (int i = 0; i < tempCanvasGroup.Length; i++) {
            tempGameObjects[i] = tempCanvasGroup[i].gameObject;
            if (tempGameObjects[i].name.Contains("Pause"))
                m_pauseMenuUI = tempGameObjects[i];
            else if (tempGameObjects[i].name.Contains("Settings"))
                m_settingsMenuUI = tempGameObjects[i];
        }
        m_pauseMenuCanvasGroup = m_pauseMenuUI.GetComponent<CanvasGroup>();
        m_pauseMenuAnimator = m_pauseMenuUI.GetComponent<Animator>();

        GameObject.Find("InputSystem").GetComponent<PlayerInput>().actions["TogglePauseMenu"].performed += _ => TogglePauseMenu();
    }

    public void Resume() {
        m_pauseMenuAnimator.enabled = true;
        m_pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        m_pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void TogglePauseMenu() {
        if (GameIsPaused) {
            Resume();
        }
        else
            Pause();
    }

    public void gotoSettingsMenu() {
        m_pauseMenuCanvasGroup.alpha = 0;
        m_pauseMenuCanvasGroup.interactable = false;
        m_settingsMenuUI.SetActive(true);
    }

    public void returnToPauseMenu() {
        m_settingsMenuUI.SetActive(false);
        m_pauseMenuCanvasGroup.alpha = 1;
        m_pauseMenuCanvasGroup.interactable = true;
    }

    public void LoadMenu() {
        Debug.Log("Loading Menu...");
    }

    public void QuitGame() {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
