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
            TowerController.Instance._onTowerWasBought2 += CloseMenue;

            CloseMenue();
        }

        private void OnDestroy() {
            TowerController.Instance._onTowerWasBought2 -= CloseMenue;
        }

        public override void OpenMenue() {
            SetTowerInfo();
            base.OpenMenue();
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
                break;
                case csTowerBaseScript.CurrencyEnum.C6:
                    _gold.text = "0";
                    _c6.text = tower.GetBuildCosts().ToString();
                    _neoplasma.text = "0";
                    break;
                case csTowerBaseScript.CurrencyEnum.Neoplasma:
                    _gold.text = "0";
                    _c6.text = "0";
                    _neoplasma.text = tower.GetBuildCosts().ToString();
                    break;
            }
        }       
    }
}
