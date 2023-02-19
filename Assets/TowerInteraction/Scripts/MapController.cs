using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class MapController : Utils.Singleton<MapController> {
    [Header("Tilemaps")]
    [SerializeField] private Grid _grid;
    [SerializeField] private Tilemap _interactiveMap;
    [Header("Tiles")]
    [SerializeField] private Tile _hoverTile;
    [Header("Controller")]
    [SerializeField] private MenueController _menueController;
    [Header("Hotkeys")]
    public InputActionReference _leftMouseClick_Tower;
    public InputActionReference _leftMouseClick_Map;

    private float _offsetOfPrefabToTile = 0.5f;
    public Vector3Int _previousTileMapMousePosition { get; private set; }
    public GameObject _selectedTower { get; private set; }

    private void Awake() {
        TowerController.Instance._onTowerWasBought += EneableController;
        TowerController.Instance._onTowerWasDestroyed += EneableController;

        _leftMouseClick_Tower.action.performed += OpenTowerMenue;
        _leftMouseClick_Map.action.performed += OpenShopMenue;
    }
    private void OnDestroy() {
        TowerController.Instance._onTowerWasBought -= EneableController;
        TowerController.Instance._onTowerWasDestroyed -= EneableController;
    }

    private void OnEnable() {
        _leftMouseClick_Tower.action.Enable();
        _leftMouseClick_Map.action.Enable();
    }

    private void OnDisable()
    {
        _leftMouseClick_Tower.action.Disable();
        _leftMouseClick_Map.action.Disable();
    }


    private void Update() {
        Vector3Int currentMousePosition = GetMousePosition();

        //Hover effect
        if (!currentMousePosition.Equals(_previousTileMapMousePosition))
        {
            //Reset the old tile
            _interactiveMap.SetTile(_previousTileMapMousePosition, null);
            //Set the new tile
            _interactiveMap.SetTile(currentMousePosition, _hoverTile);
            //Set the new mouse position
            _previousTileMapMousePosition = currentMousePosition;
        }

    }

    #region Menue
    private void OpenShopMenue(InputAction.CallbackContext context) {
        if (context.phase != InputActionPhase.Performed) { return; }

        if (!CheckIfMouseIsOverTower()) {
            DisableController();
            _menueController.onOpenMenue(MenueController.MenueType.ShopMenue);
        }
    }

    private void OpenTowerMenue(InputAction.CallbackContext context) {
        if (context.phase != InputActionPhase.Performed) { return; }

        if (CheckIfMouseIsOverTower()) {
            DisableController();
            _menueController.onOpenMenue(MenueController.MenueType.TowerMenue);
        }
    }
    #endregion

    private bool CheckIfMouseIsOverTower() {
        Vector2 start = new Vector2(_previousTileMapMousePosition.x + _offsetOfPrefabToTile,
                                        _previousTileMapMousePosition.y + _offsetOfPrefabToTile);
        Vector2 direction = new Vector2(0, 0);

        RaycastHit2D hit = Physics2D.Raycast(start, direction, 0.5f, LayerMask.GetMask("Tower"));

        if(!hit) { return false; }

        _selectedTower = hit.collider.gameObject;

        return true;
    }

    private Vector3Int GetMousePosition() {
       var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        return _grid.WorldToCell(mousePosition);
    }

    private void EneableController() {
        this.enabled = true;
    }
    private void DisableController()
    {
        this.enabled = false;
    }
}