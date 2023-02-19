using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SelectionItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    [Header("Controller")]
    [SerializeField] private MenueController _menueController;
    [SerializeField] private MenueController.MenueType _onHoverMenueType;
    [Header("Group")]
    [SerializeField] private SelectionGroup _selectionGroup;
    [Header("Icons")]
    [SerializeField] private Sprite _defaultIcon;
    [SerializeField] private Sprite _selectedIcon;
    [Header("Color")]
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _hoveredColor;
    [Header("ToolTip")]
    [SerializeField] protected TextMeshProUGUI _toolTipHeader;
    [SerializeField] protected TextMeshProUGUI _toolTipDescription;
    [Header("Variables")]
    [SerializeField] protected bool _isSelectable;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void SetColor(Color color) {
        _image.color = color;
    }

    public void SelectItem() { 
        _image.sprite = _selectedIcon;
    }

    public void UnselectItem() {
        _image.sprite = _defaultIcon;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (_isSelectable) {
            _selectionGroup.SelectItem(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if(_selectionGroup.CurrentSelectedItem == this) { return; }
        SetColor(_hoveredColor);
        _menueController.onOpenMenue(_onHoverMenueType);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (_selectionGroup.CurrentSelectedItem == this) { return; }
        SetColor(_defaultColor);
        _menueController.OnCloseMenue(_onHoverMenueType);
    }
}
