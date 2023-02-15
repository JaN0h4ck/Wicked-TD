using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "TowerStats")]
public class TowerData : ScriptableObject
{
    #region Data
    public string TowerName;

    public float TowerRange;

    public float Damage;

    public int[] Currencies;

    public GameObject[] Ammunition;

    public float fBuildCost;

    [Tooltip("!!DO NOT USE!! Currently not in use, will be programmed later")]
    public GameObject[] Skills;



    #endregion


}
