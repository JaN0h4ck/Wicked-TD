using System;
using System.Collections;
using System.Collections.Generic;
using TowerShop;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSelectionItem : SelectionItem, IPointerEnterHandler
{
    [SerializeField] private SelectedShopItem _selectedShopItem;
    [SerializeField] private int _skillIndex;

    public new void OnPointerEnter(PointerEventData eventData)
    {
        csTowerBaseScript tower = null;
        try {
            tower = MapController.Instance._selectedTower.GetComponent<csTowerBaseScript>();
        }
        catch (Exception) {
            tower = _selectedShopItem.Object.ItemData._towerPrefab.GetComponent<csTowerBaseScript>();
        }

        var skill = tower.GetCurrentSkills()[_skillIndex].GetComponent<csSkillBaseScript>();
        _toolTipHeader.text = skill.name;
        _toolTipDescription.text = skill.GetDescription();
        base.OnPointerEnter(eventData);
    }
}
