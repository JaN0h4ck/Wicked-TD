using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private CanvasGroup m_canvasGroup;

    [SerializeField]
    private float m_fadeDuration = 0.5f;

    private void Start() {
        m_canvasGroup = GetComponent<CanvasGroup>();
        Nexus.instance.onGameOver += GameOver;
    }

    private void GameOver() {
        StartCoroutine(fadeinCanvas());
    }

    private IEnumerator fadeinCanvas() {
        float t = 0;
        while (t < m_fadeDuration) {
            t += Time.deltaTime;
            m_canvasGroup.alpha = Mathf.Lerp(0, 1, t / m_fadeDuration);
            yield return null;
        }
        m_canvasGroup.interactable = true;
    }

    public void QuitGame() {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
