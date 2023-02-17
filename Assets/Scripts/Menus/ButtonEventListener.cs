using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventListener : MonoBehaviour {
    private MyButton m_button;
    private GameObject m_text;

    private void Start() {
        m_button = GetComponent<MyButton>();
        m_text = m_button.transform.GetChild(0).gameObject;
        m_button.onDown.AddListener(OnButtonDown);
        m_button.onUp.AddListener(OnButtonUp);
    }

    private void OnButtonDown() {
        if (m_text) {
            Vector3 temp = m_text.transform.position;
            temp.y -= 12.5f;
            m_text.transform.position = temp;
        }
    }

    private void OnButtonUp() {
        if (m_text) {
            Vector3 temp = m_text.transform.position;
            temp.y += 12.5f;
            m_text.transform.position = temp;
        }
    }
}
