using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AmmunitionSelectionItem : SelectionItem
{
    [SerializeField] private int _ammunitionIndex;

    public new void OnPointerEnter(PointerEventData eventData)
    {
        var tower = MapController.Instance._selectedTower.GetComponent<csTowerBaseScript>();
        //var skill = tower.
        //_toolTipHeader.text = 
        //_toolTipDescription.text =
        base.OnPointerEnter(eventData);
    }
}
