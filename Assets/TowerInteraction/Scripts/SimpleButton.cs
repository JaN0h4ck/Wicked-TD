using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SimpleButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
    [Header("Button Colors")]
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _disabledColor;
    [SerializeField] private Color _pressedColor;
    [SerializeField] private Color _hoveredColor;
    [Header("Text Colors")]
    [SerializeField] private Color _defaultTextColor;
    [SerializeField] private Color _pressedTextColor;
    [Space][Space]
    public UnityEvent onClick;

    private Image _image;
    private TextMeshProUGUI _textMeshPro;

    private void Awake() {
        _image = GetComponent<Image>();
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable() {
        SetImageColor(_activeColor);
    }

    private void OnDisable() {
        SetImageColor(_disabledColor);
    }

    public void OnPointerClick(PointerEventData eventData) {     
        onClick.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData) {
        SetImageColor(_pressedColor);
        SetTextColor(_pressedTextColor);
    }

    public void OnPointerUp(PointerEventData eventData) {
        SetImageColor(_activeColor);
        SetTextColor(_defaultTextColor);
    }

    public void OnPointerEnter(PointerEventData eventData)  {
        SetImageColor(_hoveredColor);
    }

    public void OnPointerExit(PointerEventData eventData) {
        SetImageColor(_activeColor);
    }

    private void SetImageColor(Color color) {
        _image.color = color;
    }

    private void SetTextColor(Color color) { 
        _textMeshPro.color = color;
    }
}