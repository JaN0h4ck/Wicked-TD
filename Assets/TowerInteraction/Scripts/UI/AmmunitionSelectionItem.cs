using System;
using System.Collections;
using System.Collections.Generic;
using TowerShop;
using UnityEngine;
using UnityEngine.EventSystems;

public class AmmunitionSelectionItem : SelectionItem, IPointerEnterHandler
{
    [SerializeField] private SelectedShopItem _selectedShopItem;
    [SerializeField] private int _ammunitionIndex;

    public new void OnPointerEnter(PointerEventData eventData)
    {
        csTowerBaseScript tower = null;
        try
        {
            tower = MapController.Instance._selectedTower.GetComponent<csTowerBaseScript>();
        }
        catch (Exception)
        {
            tower = _selectedShopItem.Object.ItemData._towerPrefab.GetComponent<csTowerBaseScript>();
        }

        var ammunition = tower.GetCurrentAmmunition()[_ammunitionIndex].GetComponent<csBullet>();
        _toolTipHeader.text = ammunition.name;
        _toolTipDescription.text = "Damage: " + ammunition.GetDamageModifier() + "\n" +
                                   "Bulletspeed: " + ammunition.GetBulletspeedModifier() + "\n" +
                                   "FireSpeed: " + -ammunition.GetFireSpeedModifier() + "\n";
        base.OnPointerEnter(eventData);
    }
}
