using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleButtonText : SimpleButton, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Text Colors")]
    [SerializeField] private Color _defaultTextColor;
    [SerializeField] private Color _pressedTextColor;

    private TextMeshProUGUI _textMeshPro;

    private new void Awake() {
        _image = GetComponent<Image>();
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    public new void OnPointerClick(PointerEventData eventData) {
        
        base.OnPointerClick(eventData);
    }

    public new void OnPointerDown(PointerEventData eventData) {
        SetTextColor(_pressedTextColor);
        base.OnPointerDown(eventData);
    }

    public new void OnPointerUp(PointerEventData eventData) {
        SetTextColor(_defaultTextColor);
        base.OnPointerUp(eventData);
    }

    public new void OnPointerEnter(PointerEventData eventData) {
        base.OnPointerEnter(eventData);
    }

    public new void OnPointerExit(PointerEventData eventData) {
        base.OnPointerExit(eventData);
    }

    private void SetTextColor(Color color) {
        _textMeshPro.color = color;
    }
}