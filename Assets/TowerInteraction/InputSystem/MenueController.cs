using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Events;
using static MenueController;

[CreateAssetMenu(fileName = "MenueController", menuName = "ScriptableObjects/Controller/MenueController")]
public class MenueController : ScriptableObject {
    [SerializeField] private GameEvent _onOpenShopMenue;
    [SerializeField] private GameEvent _onCloseShopMenue;

    [SerializeField] private GameEvent _onOpenTowerMenue;
    [SerializeField] private GameEvent _onCloseTowerMenue;

    [SerializeField] private GameEvent _onOpenTowerInfo;
    [SerializeField] private GameEvent _onCloseTowerInfo;

    [SerializeField] private GameEvent _onOpenPauseMenue;
    [SerializeField] private GameEvent _onClosePauseMenue;

    [SerializeField] private GameEvent _onOpenTowerMenueToolTip;
    [SerializeField] private GameEvent _onCloseTowerMenueToolTip;

    [SerializeField] private GameEvent _onOpenTowerInfoToolTip;
    [SerializeField] private GameEvent _onCloseTowerInfoToolTip;

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
            case MenueType.PauseMenue:
                _onOpenPauseMenue.Invoke();
            break;
            case MenueType.TowerMenueToolTip:
                _onOpenTowerMenueToolTip.Invoke();
            break;
            case MenueType.TowerInfoToolTip:
                _onOpenTowerInfoToolTip.Invoke();
            break;
        }
    }

    public void OnCloseMenue(MenueType menueType) {
        switch (menueType)
        {
            case MenueType.ShopMenue:
                _onCloseShopMenue.Invoke();
            break;
            case MenueType.TowerMenue:
                _onCloseTowerMenue.Invoke();
            break;
            case MenueType.TowerInfo:
                _onCloseTowerInfo.Invoke();
            break;
            case MenueType.PauseMenue:
                _onClosePauseMenue.Invoke();
            break;
            case MenueType.TowerMenueToolTip:
                _onCloseTowerMenueToolTip.Invoke();
            break;
            case MenueType.TowerInfoToolTip:
                _onCloseTowerInfoToolTip.Invoke();
            break;
        }
    }

    public enum MenueType { 
        ShopMenue = 0,
        TowerMenue = 1,
        TowerInfo = 2,
        PauseMenue = 3,
        TowerMenueToolTip = 4,
        TowerInfoToolTip = 5,
    }
}
