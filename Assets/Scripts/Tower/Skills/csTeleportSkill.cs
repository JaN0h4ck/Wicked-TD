using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTeleportSkill : csSkillBaseScript
{
    #region Variables

    [SerializeField]
    [Tooltip("This needs to be setup manually on the map. Just Place an empty where you want the enemies to be teleported to")]
    private GameObject gTeleportEmpty;

    #endregion
    void Start()
    {
        if(gTeleportEmpty==null)
        {
            Debug.LogError("(csTeleportSkill): gTeleportEmpty is not etup. Enemies cant be teleported anywhere");
        }
        DestroyTimer();
    }


    private void TeleportEnemiesInRange()
    {
       /* Collider2D[] hitColliders = Physics2D.OverlapBoxAll(this.transform.position, v2Size, 0.0f, ShootScript.GetEnemieLayer());
        foreach (Collider2D cd in hitColliders)
        {

        }*/
    }
    
}
