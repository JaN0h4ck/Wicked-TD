using System;
using System.Collections;
using System.Collections.Generic;
using TowerShop;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerController : Utils.Singleton<TowerController> {
    [SerializeField] private SelectedShopItem _selectedShopItem;

    private float _offsetOfPrefabToTile = 0.5f;

    public event Action _onTowerWasBought;
    public event Action _onTowerWasDestroyed;

    public void BuyTower() {
        if (_selectedShopItem.Object.ItemData._towerPrefab == null) {
            Debug.LogError("Tryed to spawn a tower but there was no tower prefab selected");
        }

        GameObject instantiatedTower = Instantiate(_selectedShopItem.Object.ItemData._towerPrefab);

        Vector3 newPosition = MapController.Instance._previousTileMapMousePosition;
        newPosition.x += _offsetOfPrefabToTile;
        newPosition.y += _offsetOfPrefabToTile;

        instantiatedTower.transform.position = newPosition;

        _onTowerWasBought?.Invoke();
    }

    public void DestroyTower() {
        Vector2 start = new Vector2(MapController.Instance._previousTileMapMousePosition.x + _offsetOfPrefabToTile,
                                        MapController.Instance._previousTileMapMousePosition.y + _offsetOfPrefabToTile);
        Vector2 direction = new Vector2(0, 0);
        RaycastHit2D hit = Physics2D.Raycast(start, direction, _offsetOfPrefabToTile);

        if (hit) { 
            Destroy(hit.collider.gameObject);
            _onTowerWasDestroyed?.Invoke();
        }
    }
}