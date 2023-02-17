using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTeleportSkill : csSkillBaseScript
{
    #region Variables

    //This needs to be setup manually on the map. Just Place an empty where you want the enemies to be teleported to and name it TeleporterEmpty
    private GameObject gTeleportEmpty;

    private csWeapon WeaponManager;

    private GameObject gCloneObject;

    private List<GameObject> glSaveObjects;
    private Collider2D[] hitColliders;
    #endregion
    void Start()
    {
        gTeleportEmpty = GameObject.Find("TeleporterEmpty");
        if(gTeleportEmpty==null)
        {
            Debug.LogError("(csTeleportSkill): gTeleportEmpty is not etup. Enemies cant be teleported anywhere. Place a gameobject in scene named TeleporterEmpty");
        }
        glSaveObjects = new List<GameObject>();
        WeaponManager = this.transform.parent.gameObject.GetComponent<csWeapon>();
        gCloneObject = this.transform.GetChild(0).gameObject;

        SetupTeleportEnemiesInRange();
        Invoke("DestroyAllAnimations", fDuration);
    }


    private void SetupTeleportEnemiesInRange()
    {
        hitColliders = Physics2D.OverlapBoxAll(this.transform.position, v2Size, 0.0f, WeaponManager.GetEnemieLayer());
        foreach (Collider2D cd in hitColliders)
        {
            if (cd != null)
            {
                GameObject gTemp = cd.gameObject;
                PlayAnimationAtEnemyPosition(gTemp.transform.position);
            }
        }
        PlayAnimationAtEnemyPosition(gTeleportEmpty.transform.position);
        Invoke("TeleportEnemiesInRange", 0.4f);
        Invoke("DestroyAllAnimations",fDuration);
    }

    private void TeleportEnemiesInRange()
    {
        foreach (Collider2D cd in hitColliders)
        {
            if (cd != null)
            {
                Transform tsEnemy = cd.transform;
                tsEnemy.position = gTeleportEmpty.transform.position;
            }
        }
    }
    #region Animation
    private void PlayAnimationAtEnemyPosition(Vector3 v3EnemyPos)
    {
        GameObject gTemp = Instantiate(gCloneObject, v3EnemyPos, Quaternion.identity);
        gTemp.transform.GetChild(0).gameObject.SetActive(true);
        gTemp.transform.GetChild(1).gameObject.SetActive(true);
        glSaveObjects.Add(gTemp);
    }
    
    private void DestroyAllAnimations()
    {
        for (int i = 0; i < glSaveObjects.Count; i++)
        {
            Destroy(glSaveObjects[i]);
        }
        Destroy(this.gameObject);
    }
    #endregion
}
