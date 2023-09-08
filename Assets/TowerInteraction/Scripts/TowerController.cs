using System;
using System.Collections;
using System.Collections.Generic;
using TowerShop;
using UnityEngine;
using UnityEngine.InputSystem;
using static csTowerBaseScript;

public class TowerController : Utils.Singleton<TowerController> {
    [SerializeField] private SelectedShopItem _selectedShopItem;

    private float _offsetOfPrefabToTile = 0.5f;

    public event Action _onTowerWasBought2;
    public event Action _onTowerWasDestroyed;

    public void BuyTower() {
        if (_selectedShopItem.Object.ItemData._towerPrefab == null) {
            Debug.LogError("Tryed to spawn a tower but there was no tower prefab selected");
        }

        csTowerBaseScript tower = _selectedShopItem.Object.ItemData._towerPrefab.GetComponent<csTowerBaseScript>();

        CurrencyEnum currency = tower.GetCurrencyType();
        Currency Coin;
        int amount = (int)tower.GetBuildCosts();

        string coinType = "";
        switch (currency)
        {
            case (CurrencyEnum.Gold):
                coinType = "Gold";
                break;
            case (CurrencyEnum.C6):
                coinType = "C6";
                break;
            case (CurrencyEnum.Neoplasma):
                coinType = "Neoplasma";
                break;
        }

        Shop.Instance.currencyMap.TryGetValue(coinType, out Coin);

        if (Coin.GetBalance() - amount > 0) {
            //Buy tower
            GameObject instantiatedTower = Instantiate(_selectedShopItem.Object.ItemData._towerPrefab);

            Vector3 newPosition = MapController.Instance._previousTileMapMousePosition;
            newPosition.x += _offsetOfPrefabToTile;
            newPosition.y += _offsetOfPrefabToTile;

            instantiatedTower.transform.position = newPosition;

            _onTowerWasBought2?.Invoke();
        }            
    }

    public void DestroyTower() {
        Vector2 start = new Vector2(MapController.Instance._previousTileMapMousePosition.x + _offsetOfPrefabToTile,
                                        MapController.Instance._previousTileMapMousePosition.y + _offsetOfPrefabToTile);
        Vector2 direction = new Vector2(0, 0);
        RaycastHit2D hit = Physics2D.Raycast(start, direction, _offsetOfPrefabToTile);
        _onTowerWasDestroyed?.Invoke();

        if (hit) {
            GameObject hitObject = hit.collider.gameObject;
            hitObject.GetComponent<csTowerBaseScript>().OnTowerDeath();
            Destroy(hitObject);
        }
    }
}