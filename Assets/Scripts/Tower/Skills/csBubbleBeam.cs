using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csBubbleBeam : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private GameObject gBubbleEffectPrefab;

    private csTransformations2D EffectTransformation;
    private csWeapon ShootScript;

    #endregion

    #region Setup

    private void Start()
    {
        ShootScript = this.gameObject.GetComponent<csWeapon>();
    }

    #endregion

    private void SpawnBubbleAnimation()
    {
        GameObject gEffect= Instantiate(gBubbleEffectPrefab, this.transform.position, Quaternion.identity);
        SetDirection(gEffect);
    }

    #region Helper
    private void SetDirection(GameObject gEffect)
    {
        GameObject temp = new GameObject();
        temp.transform.position = ShootScript.GetLastEnemyPos();
        EffectTransformation.LookAt2D(gEffect.transform, temp.transform);
        Destroy(temp);
    }

    private void GetEffectTransformationScript(GameObject gEffect)
    {
        if(gEffect.GetComponent<csTransformations2D>()==null)
        {
            EffectTransformation = gEffect.AddComponent<csTransformations2D>();
        }
        else
        {
            EffectTransformation = gEffect.GetComponent<csTransformations2D>();
        }
    }
    #endregion


}
