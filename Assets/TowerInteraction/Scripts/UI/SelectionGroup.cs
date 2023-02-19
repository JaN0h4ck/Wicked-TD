using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionGroup : MonoBehaviour {

    private SelectionItem _currentSelectedItem;

    public SelectionItem CurrentSelectedItem { 
        get { return _currentSelectedItem; }
        private set { _currentSelectedItem = value; }
    }

    public void SelectItem(SelectionItem item) {
        UnselectItem();
        CurrentSelectedItem = item;
        CurrentSelectedItem.SelectItem();
    }

    private void UnselectItem() { 
        if(CurrentSelectedItem == null ) { return; }
        CurrentSelectedItem.UnselectItem();
    }
}
