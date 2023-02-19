using UnityEngine;
using Utils.Menue;

namespace TowerShop {
    public class ShopMenueController : MenueNavigation
    {
        [Header("Shop")]
        [SerializeField] private ShopItem _shopItemPrefab;
        [SerializeField] private RectTransform _itemContainer;
        [SerializeField] private ShopContent _shopContent;

        private float _itemContainerSizeAdaption = 144f;

        private void Awake()
        {
            TowerController.Instance._onTowerWasBought2 += CloseMenue;

            CloseMenue();
        }

        private void OnDestroy()
        {
            TowerController.Instance._onTowerWasBought2 -= CloseMenue;
        }

        private void Start() {
            for (int i = 0; i < _shopContent.list.Count; i++) {
                CreateNewListItem(_shopContent.list[i]);

                //Adapt the content size for each new row if theres more than 8 items in the shop
                if(i + 1 > 8 && i + 1 % 4 == 0) {  
                    RectTransform rectTransform = (RectTransform)_itemContainer.transform;
                    rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 
                                                          rectTransform.sizeDelta.y + _itemContainerSizeAdaption);
                }
            }
        }

        private void CreateNewListItem(ShopItemData itemData) {
            var newItem = Instantiate(_shopItemPrefab);
            //Place it in the contatiner; tell it to not keep it's current position or scale
            newItem.transform.SetParent(_itemContainer, worldPositionStays: false);
            //Set the data
            newItem.GetComponent<ShopItem>().ItemData = itemData;
        }
    }
}