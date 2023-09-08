using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class csExplosionTower : csTowerBaseScript {
    #region Variables
    [SerializeField]
    private GameObject gExplosionEffectPrefab;

    [SerializeField]
    private GameObject gDamageIndicatorPrefab;

    [SerializeField]
    private float fExplosionDamage;

    public static float fExploionRange = 10;

    #endregion

    public override void OnTowerDeath() {
        base.OnTowerDeath();
        PLayExplosionEffect();
        ExplosionDamage();
    }

    #region Explosion
    protected void PLayExplosionEffect()
    {
        Instantiate(gExplosionEffectPrefab, this.transform.position, Quaternion.identity);
    }

    protected void ExplosionDamage()
    {
        DoDamageToTarget(GetHitEnemies());   
    }


    private List<Transform> GetHitEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, (transform.localScale / 2) * fExploionRange, 0.0f, WeaponManager.GetEnemieLayer());
        List<Transform> tslHitEnemies = new List<Transform>();
        foreach(Collider2D cd in hitColliders)
        {
            tslHitEnemies.Add(cd.gameObject.transform);
            Debug.LogWarning("/HitEnemy: " + cd.gameObject.name);
        }
        return tslHitEnemies;
    }
    private void DoDamageToTarget(List<Transform> tslHitEnemies)
    {
        //do damage!! To do
        WeaponManager.SetupTargetsIfNecessary(tslHitEnemies);

        foreach (Transform tsEnemy in tslHitEnemies)
        {
            if (tsEnemy.gameObject.GetComponent<csEnemyHealth>() != null)
            {
                tsEnemy.gameObject.GetComponent<csEnemyHealth>().LooseHealth(fExplosionDamage);
            }
            GameObject gTemp = Instantiate(gDamageIndicatorPrefab, tsEnemy.transform.position, Quaternion.identity);
            gTemp.GetComponent<TextMeshPro>().text = fExplosionDamage.ToString();
        }
    }
    #endregion
}
