using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPoisonSkill : csSkillBaseScript
{

    private csWeapon ShootScript;
    [SerializeField]
    private float fDamageOverTime;

    void Start()
    {
        ShootScript = transform.parent.gameObject.GetComponent<csWeapon>();
        PoisonEnemies();
        Invoke("StopEffect", 2.0f);
    }

    private void PoisonEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, v2Size, 0.0f, ShootScript.GetEnemieLayer());
        List<Transform> tslEnemies = new List<Transform>();
        foreach (Collider2D cd in hitColliders)
        {
            if (cd != null)
            {
                Debug.LogWarning("Hit" + cd.name);
                tslEnemies.Add(cd.transform);
            }
        }
        ShootScript.SetupTargetsIfNecessary(tslEnemies);
        foreach (Transform tsEnemy in tslEnemies)
        {
            if (tsEnemy != null)
            {
                csPosionDamage temp =  tsEnemy.gameObject.AddComponent<csPosionDamage>();
                temp.SetDamageOverTimeAndDuration(fDamageOverTime,fDuration);
                temp.StartEffect();
            }
        }
    }

    private void StopEffect()
    {
        Destroy(this.gameObject);
    }

}
