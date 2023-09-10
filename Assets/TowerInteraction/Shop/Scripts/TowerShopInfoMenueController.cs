using TMPro;
using UnityEngine;
using Utils.Menue;

namespace TowerShop {
    public class TowerShopInfoMenueController : MenueNavigation {

        [SerializeField] private SelectedShopItem _selectedShopItem;

        [Header("Infos")]
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _damage;
        [SerializeField] private TextMeshProUGUI _attackSpeed;
        [SerializeField] private TextMeshProUGUI _range;
        [Header("Ammunition")]
        [Header("Skills")]
        [Header("Currency")]
        [SerializeField] private TextMeshProUGUI _gold;
        [SerializeField] private TextMeshProUGUI _c6;
        [SerializeField] private TextMeshProUGUI _neoplasma;
        [Header("UI")]
        [SerializeField] private GameObject _buyButton;
        [SerializeField] private TextMeshProUGUI _notEnoughWarning;

        private void Awake() {
            TowerController.Instance._onTowerWasBought2 += CloseMenue;

            CloseMenue();
        }


        public override void OpenMenue() {
            SetTowerInfo();
            base.OpenMenue();
        }

        public override void CloseMenue() {
            _buyButton.SetActive(true);
            _notEnoughWarning.gameObject.SetActive(false);
            base.CloseMenue();
        }

        private void SetTowerInfo() {
            var tower = _selectedShopItem.Object.ItemData._towerPrefab.GetComponent<csTowerBaseScript>();

            //Infos
            _name.text = tower.sTowerName;
            _damage.text = tower.GetDamage().ToString();
            _attackSpeed.text = tower.GetFireSpeed().ToString();
            _range.text = tower.GetRange().ToString();
            //Ammunition
            //Skills
            //Currency
            switch(tower.GetCurrencyType()){
                case csTowerBaseScript.CurrencyEnum.Gold:
                    _gold.text = tower.GetBuildCosts().ToString();
                    _c6.text = "0";
                    _neoplasma.text = "0";
                    HandleBuyButton("Gold", tower.GetBuildCosts());
                    break;
                case csTowerBaseScript.CurrencyEnum.C6:
                    _gold.text = "0";
                    _c6.text = tower.GetBuildCosts().ToString();
                    _neoplasma.text = "0";
                    HandleBuyButton("C6", tower.GetBuildCosts());
                    break;
                case csTowerBaseScript.CurrencyEnum.Neoplasma:
                    _gold.text = "0";
                    _c6.text = "0";
                    _neoplasma.text = tower.GetBuildCosts().ToString();
                    HandleBuyButton("Neoplasma", tower.GetBuildCosts());
                    break;
            }
        }

        private void HandleBuyButton(string currency, float towerBuildCost) {
            Currency tempCurrency;
            if (!Shop.Instance.currencyMap.TryGetValue(currency, out tempCurrency))
                return;
            if (towerBuildCost > tempCurrency.GetBalance()) {
                _buyButton.SetActive(false);
                _notEnoughWarning.gameObject.SetActive(true);
                _notEnoughWarning.text = "Not enough " + currency + "!";
            }
            else {
                _buyButton.SetActive(true);
                _notEnoughWarning.gameObject.SetActive(false);
            }
        }
    }
}
