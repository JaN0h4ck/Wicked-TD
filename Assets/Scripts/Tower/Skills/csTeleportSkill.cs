using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTeleportSkill : csSkillBaseScript
{
    #region Variables

    [SerializeField]
    [Tooltip("This needs to be setup manually on the map. Just Place an empty where you want the enemies to be teleported to")]
    private GameObject gTeleportEmpty;

    private csWeapon WeaponManager;

    private GameObject gCloneObject;

    private List<GameObject> glSaveObjects;
    #endregion
    void Start()
    {
        if(gTeleportEmpty==null)
        {
            Debug.LogError("(csTeleportSkill): gTeleportEmpty is not etup. Enemies cant be teleported anywhere");
        }
        glSaveObjects = new List<GameObject>();
        WeaponManager = this.transform.parent.gameObject.GetComponent<csWeapon>();
        gCloneObject = this.transform.GetChild(0).gameObject;


        DestroyTimer();
        Invoke("DestroyAllAnimations", fDuration);
    }


    private void SetupTeleportEnemiesInRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(this.transform.position, v2Size, 0.0f, WeaponManager.GetEnemieLayer());
        foreach (Collider2D cd in hitColliders)
        {
            GameObject gTemp = cd.gameObject;
            PlayAnimationAtEnemyPosition(gTemp.transform.position);
        }
        Invoke("TeleportEnemiesInRange", 0.1f);
    }

    private void TeleportEnemiesInRange(Collider2D[] cdaEnemies)
    {

    }
    private void PlayAnimationAtEnemyPosition(Vector3 v3EnemyPos)
    {
        GameObject gTemp = Instantiate(gCloneObject, v3EnemyPos, Quaternion.identity);
        glSaveObjects.Add(gTemp);
    }
    
    private void DestroyAllAnimations()
    {
        for (int i = 0; i < glSaveObjects.Count; i++)
        {
            Destroy(glSaveObjects[i]);
        }
    }
}
