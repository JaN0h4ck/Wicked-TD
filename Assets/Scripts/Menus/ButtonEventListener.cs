using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonEventListener : MonoBehaviour {
    private MyButton m_button;
    private GameObject m_text;

    private float m_ButtonDisplacementMultiplier = 0.0045f;

    private void Start() {
        m_button = GetComponent<MyButton>();
        m_text = m_button.transform.GetChild(0).gameObject;
        m_button.onDown.AddListener(OnButtonDown);
        m_button.onUp.AddListener(OnButtonUp);
    }

    private void OnButtonDown() {
        if (m_text) {
            Vector3 tempVector = m_text.transform.position;
            tempVector.y -= (float)Screen.currentResolution.height * m_ButtonDisplacementMultiplier;
            //tempVector.y -= 12.5f;
            m_text.transform.position = tempVector;

            float h, s, v;
            Color tempColor = m_text.GetComponent<TextMeshProUGUI>().color;
            Color.RGBToHSV(tempColor, out h, out s, out v);
            v -= 10.0f;
            m_text.GetComponent<TextMeshProUGUI>().color = Color.HSVToRGB(h, s, v);
        }
    }

    private void OnButtonUp() {
        if (m_text) {
            Vector3 temp = m_text.transform.position;
            temp.y += (float)Screen.currentResolution.height * m_ButtonDisplacementMultiplier;
            m_text.transform.position = temp;

            float h, s, v;
            Color tempColor = m_text.GetComponent<TextMeshProUGUI>().color;
            Color.RGBToHSV(tempColor, out h, out s, out v);
            v += 10.0f;
            m_text.GetComponent<TextMeshProUGUI>().color = Color.HSVToRGB(h, s, v);
        }
    }
}
