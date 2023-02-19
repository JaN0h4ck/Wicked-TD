using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSelectionItem : SelectionItem, IPointerEnterHandler
{
    [SerializeField] private int _skillIndex;

    public new void OnPointerEnter(PointerEventData eventData)
    {
        var tower = MapController.Instance._selectedTower.GetComponent<csTowerBaseScript>();
        //var skill = tower.
        //_toolTipHeader.text = 
        //_toolTipDescription.text =
        base.OnPointerEnter(eventData);
    }
}
