using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csBubbleBeam : csSkillBaseScript
{
    #region Variables
  
    private int iSkillIndex;

    [SerializeField]
    private float fDamage;

    private csTransformations2D EffectTransformation;
    private csWeapon ShootScript;
    private GameObject gMainObject;

    private csDamageManager DamageIndicator;

    private Vector3 v3SaveTargetPosition;
    #endregion

    #region Setup

    private void Start()
    {
        Invoke("Setup", 0.1f);
    }

    private void Setup()
    {
        gMainObject = this.transform.parent.gameObject;
        EffectTransformation = gameObject.GetComponent<csTransformations2D>();
        ShootScript = gMainObject.GetComponent<csWeapon>();
        DamageIndicator = csDamageManager.current;
        SetDirection();
        StartCoroutine(DoDamageOverTime());
    }

    #endregion
    
    private IEnumerator DoDamageOverTime()
    {
        while(fDuration>0)
        {
            DamageEnemies();
            fDuration -= 0.4f;
            yield return new WaitForSecondsRealtime(0.4f);
        }
        Destroy(this.gameObject);
    }

    private void DamageEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(Vector3.MoveTowards(this.transform.position, v3SaveTargetPosition, 0.5f), v2Size, 0.0f, ShootScript.GetEnemieLayer());
        List<Transform> tslEnemies = new List<Transform>();
        foreach(Collider2D cd in hitColliders)
        {
            Debug.LogWarning("Hit" + cd.name);
            tslEnemies.Add(cd.transform);
        }
        ShootScript.SetupTargetsIfNecessary(tslEnemies);
        foreach(Transform tsEnemy in tslEnemies)
        {
            tsEnemy.gameObject.GetComponent<csEnemyHealth>().LooseHealth(fDamage);
            DamageIndicator.ShowDamageAt(tsEnemy.transform,fDamage);
        }
    }

    #region Helper
    private void SetDirection()
    {
        GameObject temp = new GameObject();
        temp.AddComponent<Transform>();
        temp.transform.position = ShootScript.GetLastEnemyPos();
        Debug.LogWarning(gameObject);
        Debug.LogWarning(temp);
        EffectTransformation.LookAt2D(this.transform, temp.transform);
        this.transform.Rotate(0, 0, 180);
        this.transform.position = Vector3.MoveTowards(this.transform.position,temp.transform.position,1);
        v3SaveTargetPosition = temp.transform.position;
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
