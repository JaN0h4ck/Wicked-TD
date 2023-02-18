using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils.Events;

namespace TowerShop {
    [RequireComponent(typeof(Image))]
    public class ShopItem : MonoBehaviour, IPointerClickHandler {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private SelectedShopItem _selectedShopItem;
        [SerializeField] private MenueController _menueController;

        private ShopItemData _itemData; 
        public ShopItemData ItemData {
            get {
                return _itemData;
            }
            set {
                _itemData = value;
                _itemIcon.sprite = value._towerIcon;
            }
        }

        public void OnPointerClick(PointerEventData eventData) {
            _selectedShopItem.Object = this;
            _menueController.onOpenMenue(MenueController.MenueType.TowerInfo);
        }
    }

    [Serializable]
    public class ShopItemData {
        public GameObject _towerPrefab;
        public Sprite _towerIcon;
        public TowerData _towerData;
    }
}