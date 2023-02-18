using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class csDamageManager : MonoBehaviour
{
    #region Singleton

    public static csDamageManager current;

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
    private GameObject gDamageIndicatorPrefab;

    [SerializeField]
    private GameObject gIndicatorEmpty;

    #endregion

    public void ShowDamageAt(Transform tsDamagedObject, float fDamage)
    {
        GameObject gTemp = Instantiate(gDamageIndicatorPrefab, tsDamagedObject.position, Quaternion.identity);
        gTemp.GetComponent<TextMeshPro>().text = fDamage.ToString();
        gTemp.transform.SetParent(gIndicatorEmpty.transform);
    }
}
