using System.Collections;
using System.Collections.Generic;
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

        private void Awake() {
            TowerController.Instance._onTowerWasBought += CloseMenue;

            CloseMenue();
        }

        private void OnDestroy() {
            TowerController.Instance._onTowerWasBought -= CloseMenue;
        }

        public override void OpenMenue() {
            SetTowerInfo();
            base.OpenMenue();
        }

        private void SetTowerInfo() {
            TowerData towerData = _selectedShopItem.Object.ItemData._towerData;

            //Infos
            _name.text = towerData.TowerName;
            _damage.text = towerData.Damage.ToString();
            //_attackSpeed.text = towerData.
            _range.text = towerData.TowerRange.ToString();
            //Ammunition
            //Skills
            //Currency
            //_gold.text = towerData.Currencies[0].ToString();
            //_c6.text = towerData.Currencies[1].ToString();
            //_neoplasma.text = towerData.Currencies[2].ToString();
        }
    }
}
