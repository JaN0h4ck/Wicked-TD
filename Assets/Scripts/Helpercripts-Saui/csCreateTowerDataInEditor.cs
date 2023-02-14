using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class csCreateTowerDataInEditor
{

    
    [MenuItem("TowerData/CreateDatafile %g")]
    static void UpdatePaths()
    {
        int iScriptIndex = GetScriptIndex();
        string sFileName = Selection.activeObject.name;
        
        string sCurrentPath = "Assets/Prefabs/Tower/BasicTowers/TowerData/"+sFileName+".asset";


        TowerData tdTowerData = ScriptableObject.CreateInstance<TowerData>();
        AssetDatabase.CreateAsset(tdTowerData, sCurrentPath);
        AssetDatabase.SaveAssets();

        GameObject gTemp = (GameObject)Selection.activeObject;

        Debug.LogWarning(iScriptIndex);
        switch (iScriptIndex)
        {
            case (0):
                Debug.LogWarning("SettingUpOne");
                csTowerBaseScript tempo = gTemp.GetComponent<csTowerBaseScript>();
                tdTowerData.TowerName = gTemp.name;
                tdTowerData.Damage = tempo.GetDamage();
                tdTowerData.TowerRange = tempo.GetRange();
                tdTowerData.Currencies = tempo.GetPoints();
                tdTowerData.fBuildCost = tempo.GetBuildCosts();
                //skills u ammo fehlt
                break;
            case (1):
                Debug.LogWarning("SettingUpOne");
                csNormalTower tempn = gTemp.GetComponent<csNormalTower>();
                tdTowerData.Damage = tempn.GetDamage();
                tdTowerData.TowerRange = tempn.GetRange();
                tdTowerData.Currencies = tempn.GetPoints();
                tdTowerData.fBuildCost = tempn.GetBuildCosts();
                //skills u ammo fehlt
                break;
            case (2):
                csExplosionTower tempp = gTemp.GetComponent<csExplosionTower>();
                tdTowerData.Damage = tempp.GetDamage();
                tdTowerData.TowerRange = tempp.GetRange();
                tdTowerData.Currencies = tempp.GetPoints();
                tdTowerData.fBuildCost = tempp.GetBuildCosts();
                //skills u ammo fehlt
                break;
            case (3):
                csEconomyTower tempz = gTemp.GetComponent<csEconomyTower>();
                tdTowerData.Damage = tempz.GetDamage();
                tdTowerData.TowerRange = tempz.GetRange();
                tdTowerData.Currencies = tempz.GetPoints();
                tdTowerData.fBuildCost = tempz.GetBuildCosts();
                //skills u ammo fehlt
                break;
        }
        GameObject pref = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/Interfaces/TowerInterface.prefab");
        pref.GetComponent<csTowerInterface>().tdlAllTowerDataFiles.Add(tdTowerData);
    }
    static private int GetScriptIndex()
    {
        GameObject gTemp = (GameObject)Selection.activeObject;
        Debug.LogWarning(gTemp.name);
        if (gTemp.GetComponent<csTowerBaseScript>()!=null)
        {
            return 0;
        }
        if (gTemp.GetComponent<csNormalTower>() != null)
        {
            return 1;
        }
        if (gTemp.GetComponent<csExplosionTower>() != null)
        {
            return 2;
        }
        if (gTemp.GetComponent<csEconomyTower>() != null)
        {
            return 3;
        }
        return -1;
    }
 
}
   