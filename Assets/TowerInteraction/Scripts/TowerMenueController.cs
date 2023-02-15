using TMPro;
using UnityEngine;

public class TowerMenueController : MonoBehaviour
{
    [SerializeField] private GameObject m_TowerMenue;

    [SerializeField] private TextMeshProUGUI m_towerName;
    [SerializeField] private TextMeshProUGUI m_damage;
    [SerializeField] private TextMeshProUGUI m_attackSpeed;
    [SerializeField] private TextMeshProUGUI m_range;
    [SerializeField] private TextMeshProUGUI m_gold;
    [SerializeField] private TextMeshProUGUI m_c6;
    [SerializeField] private TextMeshProUGUI m_neoplasma;

    private void Awake()
    {
        CloseTowerMenue();
    }

    public void CloseTowerMenue() {
        m_TowerMenue.SetActive(false);
    }

    public void OpenTowerMenue() {
        InitializeTowerMenue();
        m_TowerMenue.SetActive(true);
    }

    /// <summary>
    /// Sets the tower menue text fields for the current selected tower
    /// </summary>
    private void InitializeTowerMenue() {
        //TODO!!!!!!!!!!!!!!
        m_towerName.text = Random.Range(0, 10).ToString();             
        //m_damage.text;
        //m_attackSpeed.text;
        //m_range.text;
        //m_gold.text;
        //m_c6.text;
        //m_neoplasma.text;
    }

    private void UpdateTowerMenue() {
        //m_gold.text;
        //m_c6.text;
        //m_neoplasma.text;
    }
}
