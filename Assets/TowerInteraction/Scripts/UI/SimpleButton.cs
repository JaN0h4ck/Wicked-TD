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
    [Space][Space]
    public UnityEvent onClick;

    protected Image _image;   

    protected void Awake() {
        _image = GetComponent<Image>();
    }

    protected void OnEnable() {
        SetImageColor(_activeColor);
    }

    protected void OnDisable() {
        SetImageColor(_disabledColor);
    }

    public void OnPointerClick(PointerEventData eventData) {     
        onClick.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData) {
        SetImageColor(_pressedColor);
    }

    public void OnPointerUp(PointerEventData eventData) {
        SetImageColor(_activeColor);
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
}