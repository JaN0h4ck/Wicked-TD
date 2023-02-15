using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class csExplosionTower : csTowerBaseScript
{
    #region Variables
    [SerializeField]
    private GameObject gExplosionEffectPrefab;

    [SerializeField]
    private GameObject gDamageIndicatorPrefab;

    [SerializeField]
    private float fExplosionDamage;

    public static float fExploionRange =5;

    #endregion
    /// <summary>
    /// In this case The on destroy Command is used to summon a explosion effect
    /// </summary>
    private void OnDestroy()
    {
        Debug.Log("(ExplosionTower): exploding " + gameObject.name);
        DropMoneyOnDeath();
        CurrencyDropAnimation();
        PLayExplosionEffect();
        ExplosionDamage();
        Debug.LogWarning(csTowerInterface.current.GetSpecificTowerData(csTowerInterface.current.NormalTower).Damage);
    }


    #region Explosion
    private void PLayExplosionEffect()
    {
        Instantiate(gExplosionEffectPrefab, this.transform.position, Quaternion.identity);
    }

    private void ExplosionDamage()
    {
        DoDamageToTarget(GetHitEnemies());   
    }


    private List<GameObject> GetHitEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, (transform.localScale / 2) * fExploionRange, 0.0f, WeaponManager.GetEnemieLayer());
        List<GameObject> glHitEnemies = new List<GameObject>();
        foreach(Collider2D cd in hitColliders)
        {
            glHitEnemies.Add(cd.gameObject);
            Debug.LogWarning("/HitEnemy: " + cd.gameObject.name);
        }
        return glHitEnemies;
    }
    private void DoDamageToTarget(List<GameObject> glHitEnemies)
    {
        //do damage!! To do
        foreach (GameObject gEnemy in glHitEnemies)
        {
            GameObject gTemp = Instantiate(gDamageIndicatorPrefab, gEnemy.transform.position, Quaternion.identity);
            gTemp.GetComponent<TextMeshPro>().text = fExplosionDamage.ToString();
        }
    }
    #endregion
}
