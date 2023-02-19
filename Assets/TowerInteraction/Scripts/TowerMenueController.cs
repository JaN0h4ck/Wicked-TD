using System.Collections;
using TMPro;
using UnityEngine;
using Utils.Menue;

public class TowerMenueController : MenueNavigation
{
    [SerializeField] private GameObject _destroyTowerIcon;
    [Header("Infos")]
    [SerializeField] private TextMeshProUGUI m_towerName;
    [SerializeField] private TextMeshProUGUI m_damage;
    [SerializeField] private TextMeshProUGUI m_attackSpeed;
    [SerializeField] private TextMeshProUGUI m_range;
    [SerializeField] private TextMeshProUGUI m_gold;
    [SerializeField] private TextMeshProUGUI m_c6;
    [SerializeField] private TextMeshProUGUI m_neoplasma;

    private csTowerBaseScript _tower;
    private csWeapon _towerWeapon;
    private float _towerDestroyIconOffsetPosY = 160f;

    private void Awake() {
        TowerController.Instance._onTowerWasDestroyed += CloseMenue;
        CloseMenue();
    }
    private void OnDestroy()
    {
        TowerController.Instance._onTowerWasDestroyed -= CloseMenue;
    }

    public override void OpenMenue() {
        InitializeTowerMenue();
        SetDestroyTowerIcon();
        base.OpenMenue();
    }

    public override void CloseMenue() { 
        StopCoroutine(UpdateTowerMenue());
        base.CloseMenue();
    }

    private void SetDestroyTowerIcon() {
        var worldPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, MapController.Instance._selectedTower.transform.position);
        var anchoredPoint = _destroyTowerIcon.transform.parent.InverseTransformPoint(worldPoint);
        anchoredPoint.y += _towerDestroyIconOffsetPosY;
        ((RectTransform)_destroyTowerIcon.transform).anchoredPosition = anchoredPoint;
    }

    private void InitializeTowerMenue() {
        _tower = MapController.Instance._selectedTower.GetComponent<csTowerBaseScript>();
        _towerWeapon = MapController.Instance._selectedTower.GetComponent<csWeapon>();

        if(_towerWeapon == null || _tower == null) {
            Debug.LogError("TowerMenue could not be loaded - TowerWeapon or tower is not assigned");
            return;
        }

        m_towerName.text = _tower.GetTowerName();
        m_damage.text = _towerWeapon.GetDamage().ToString();
        m_attackSpeed.text = _towerWeapon.GetFireSpeed().ToString();
        m_range.text = _towerWeapon.GetRange().ToString();

        m_gold.text = _tower.GetPoints()[0].ToString();
        m_c6.text = _tower.GetPoints()[1].ToString(); ;
        m_neoplasma.text = _tower.GetPoints()[2].ToString();

        StartCoroutine(UpdateTowerMenue());
    }

    private IEnumerator UpdateTowerMenue() {
        if (_towerWeapon == null || _tower == null) {
            Debug.LogError("TowerMenue could not be updated - TowerWeapon or tower is not assigned");
            yield break;
        }

        while(true) {
            m_damage.text = _towerWeapon.GetDamage().ToString();
            m_attackSpeed.text = _towerWeapon.GetFireSpeed().ToString();

            m_gold.text = _tower.GetPoints()[0].ToString();
            m_c6.text = _tower.GetPoints()[1].ToString();
            m_neoplasma.text = _tower.GetPoints()[2].ToString();

            yield return new WaitForSeconds(0.5f);
        }       
    }

    public void OnDestroyTower() {
        TowerController.Instance.DestroyTower();
    }
}