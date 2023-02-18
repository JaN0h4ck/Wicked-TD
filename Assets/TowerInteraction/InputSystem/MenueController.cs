using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Events;
using static MenueController;

[CreateAssetMenu(fileName = "MenueController", menuName = "ScriptableObjects/Controller/MenueController")]
public class MenueController : ScriptableObject {
    [SerializeField] private GameEvent _onOpenShopMenue;
    [SerializeField] private GameEvent _onColoseShopMenue;

    [SerializeField] private GameEvent _onOpenTowerMenue;
    [SerializeField] private GameEvent _onCloseTowerMenue;

    [SerializeField] private GameEvent _onOpenTowerInfo;
    [SerializeField] private GameEvent _onCloseTowerInfo;

    public void onOpenMenue(MenueType menueType) { 
        switch(menueType) {
            case MenueType.ShopMenue:
                _onOpenShopMenue.Invoke();
            break;
            case MenueType.TowerMenue:
                _onOpenTowerMenue.Invoke();
            break;
            case MenueType.TowerInfo:
                _onOpenTowerInfo.Invoke();
            break;
        }
    }

    public void OnCloseMenue(MenueType menueType) {
        switch (menueType)
        {
            case MenueType.ShopMenue:
                _onColoseShopMenue.Invoke();
                break;
            case MenueType.TowerMenue:
                _onCloseTowerMenue.Invoke();
                break;
            case MenueType.TowerInfo:
                _onCloseTowerInfo.Invoke();
                break;
        }
    }

    public enum MenueType { 
        ShopMenue = 0,
        TowerMenue = 1,
        TowerInfo = 2,
    }
}
