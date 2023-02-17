using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private GameObject m_mainMenu;
    private CanvasGroup m_mainMenuCanvasGroup;
    private GameObject m_settingsMenu;
    private CanvasGroup m_settingsMenuCanvasGroup;

    private void Start() {
        h_CanvasGroupAndGameObjectSetupForStartMethod();
    }

    #region MainMenu

    public void StartGame() {
        Debug.Log("Starting Game...");
        //Auskommentieren oder Ändern, wenn Szenenhierarchie im Build Index drin ist
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void gotoSettingsMenu() {
        m_mainMenu.SetActive(false);
        m_settingsMenu.SetActive(true);
    }

    public void ExitGame() {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

    #endregion

    #region SettingsMenu
    public void returntoMainMenu() {
        m_mainMenu.SetActive(true);
        m_settingsMenu.SetActive(false);
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    #endregion

    #region helper
    private void h_CanvasGroupAndGameObjectSetupForStartMethod() {
        CanvasGroup[] tempCanvasGroups = GetComponentsInChildren<CanvasGroup>(true);
        GameObject[] tempGameObjects = new GameObject[tempCanvasGroups.Length];

        for (int i = 0; i < tempCanvasGroups.Length; i++) {
            tempGameObjects[i] = tempCanvasGroups[i].gameObject;

            if (tempGameObjects[i].name.Contains("Main")) {
                m_mainMenuCanvasGroup = tempCanvasGroups[i];
                m_mainMenu = tempGameObjects[i];
            } else if (tempGameObjects[i].name.Contains("Settings")) {
                m_settingsMenuCanvasGroup = tempCanvasGroups[i];
                m_settingsMenu = tempGameObjects[i];
            }
        }
    }
    #endregion
}
