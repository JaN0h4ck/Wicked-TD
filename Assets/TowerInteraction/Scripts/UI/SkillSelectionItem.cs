using System;
using System.Collections;
using System.Collections.Generic;
using TowerShop;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSelectionItem : SelectionItem, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private SelectedShopItem _selectedShopItem;
    [SerializeField] private int _skillIndex;

    private bool _skillIsOnCooldown = false;

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

    public new void OnPointerClick(PointerEventData eventData) {
        if (_skillIsOnCooldown == false)
        {
            var tower = MapController.Instance._selectedTower.GetComponent<csTowerBaseScript>();

            tower.ActivateSkill(_skillIndex);

            base.OnPointerClick(eventData);

            _skillIsOnCooldown = true;

            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer() {
        float timer = UnityEngine.Random.Range(15, 30);
        yield return new WaitForSeconds(timer);
        _skillIsOnCooldown = false;
    }
}
