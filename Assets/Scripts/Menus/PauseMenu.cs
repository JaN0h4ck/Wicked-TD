using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Utils.Menue;

public class PauseMenu : MenueNavigation {

    public static bool GameIsPaused = false;
    private bool m_isSettingsMenuActive = false;

    private GameObject m_pauseMenuUI;
    private CanvasGroup m_pauseMenuCanvasGroup;
    private Animator m_pauseMenuAnimator;

    private GameObject m_settingsMenuUI;

    public static event Action onPauseMenuWasOpened;
    public static event Action onPauseMenuWasClosed;

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

        GameObject.Find("Input Controller").GetComponent<PlayerInput>().actions["TogglePauseMenu"].performed += _ => TogglePauseMenu();
    }

    private void Start() {
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    #region PauseMenu
    public override void CloseMenue() {
        m_pauseMenuAnimator.enabled = true;
        m_pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        onPauseMenuWasClosed?.Invoke();
    }

    public override void OpenMenue() {
        m_pauseMenuAnimator.enabled = true;
        m_pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        onPauseMenuWasOpened?.Invoke();
    }

    public void TogglePauseMenu() {
        if(m_isSettingsMenuActive)
            returnToPauseMenu();
        if (GameIsPaused)
            CloseMenue();
        else
            OpenMenue();
    }

    public void gotoSettingsMenu() {
        m_pauseMenuAnimator.enabled = false;
        m_isSettingsMenuActive = true;
        m_pauseMenuCanvasGroup.alpha = 0;
        m_pauseMenuCanvasGroup.interactable = false;
        m_settingsMenuUI.SetActive(true);
    }

    public void LoadMenu() {
        CloseMenue();
        StartCoroutine("LoadAsync");
    }

    private IEnumerator LoadAsync() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }

    public void QuitGame() {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
    #endregion

    #region SettingsMenu
    public void returnToPauseMenu() {
        m_isSettingsMenuActive = false;
        m_settingsMenuUI.SetActive(false);
        m_pauseMenuCanvasGroup.alpha = 1;
        m_pauseMenuCanvasGroup.interactable = true;
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }
    #endregion

    #region Deprecated
    [Obsolete("\"Resume\" is deprecated, please use \"CloseMenue()\"")]
    public void Resume() {
        m_pauseMenuAnimator.enabled = true;
        m_pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    [Obsolete("\"Pause\" is deprecated, please use \"OpenMenue()\"")]
    void Pause() {
        m_pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    #endregion
}
