using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTowerInterface : MonoBehaviour
{
    #region Singleton
    public static csTowerInterface current;

    private void Awake()
    {
        if(current==null)
        {
            current = this;
        }
    }
    #endregion

    #region Variables

    [SerializeField]
    [Tooltip("Contains all Tower prefabs")]
    private List<GameObject> glAllExistingTowerTypes;

    

    [SerializeField]
    public List<TowerData> tdlAllTowerDataFiles;
  
    public enum TowerTypes
    {
        NormalTower,
        ExplosionTower,
        EconomyTower,
        SparkyTower
    }
    public TowerTypes NormalTower = TowerTypes.NormalTower;
    public TowerTypes ExplosionTower = TowerTypes.ExplosionTower;
    public TowerTypes EconomyTower = TowerTypes.EconomyTower;
    public TowerTypes SparkyTower = TowerTypes.SparkyTower;
    #endregion

    #region Functions
    private void Start()
    {
    }

    #region Getter
    

    /// <summary>
    /// Gibt alle blueprints zu türmen aus die es gibt = alle existierenden turm basisarten
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetAllTowerPrefabs()
    {
        return glAllExistingTowerTypes;
    }

    public GameObject GetSpecificTower(TowerTypes tower)
    {
        return glAllExistingTowerTypes[EnumToIndex(tower)];
    }

    public TowerData GetSpecificTowerData(TowerTypes tower)
    {
        Debug.LogWarning(EnumToIndex(tower));
        Debug.LogWarning(tdlAllTowerDataFiles[EnumToIndex(tower)].Damage);
        return tdlAllTowerDataFiles[EnumToIndex(tower)];
    }

    public List<TowerData> GetAllTowerDatas()
    {
        return tdlAllTowerDataFiles;
    }

    private int EnumToIndex(TowerTypes tower)
    {
        switch (tower)
        {
            case (TowerTypes.NormalTower):
                return 0;
                break;
            case (TowerTypes.ExplosionTower):
                return 1;
                break;
            case (TowerTypes.EconomyTower):
                return 2;
                break;
            case (TowerTypes.SparkyTower):
                return 3;
                break;
            default:
                return -1;
                break;
        }
    }
    #endregion

    public void SpawnTowerAt(TowerTypes tower, Vector3 Position)
    {
        Instantiate(GetSpecificTower(tower), Position, Quaternion.identity);
    }

   

    #endregion
}
