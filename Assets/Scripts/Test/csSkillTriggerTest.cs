using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSkillTriggerTest : MonoBehaviour
{
    public bool bActivate = false;
    private bool bCompare = false;
    public GameObject gTowerToActivate;
    public int iSkillIndex;

    private int iBulletIndex=1;

    void Update()
    {
        if(bActivate != bCompare|| Input.GetMouseButtonDown(0))
        {
            bCompare = bActivate;
            Debug.LogWarning("(Reminder): This testscript is still active!");
            TriggerSkill();/*
            iSkillIndex++;
            if(iSkillIndex>=3)
            {
                iSkillIndex = 0;
                
            }*/
        }
        if (Input.GetMouseButtonDown(1))
        {
            ChangeBullet();
        }
    }

    private void TriggerSkill()
    {
        gTowerToActivate.GetComponent<csTowerBaseScript>().ActivateSkill(iSkillIndex);
    }

    private void ChangeBullet()
    {
        gTowerToActivate.GetComponent<csWeapon>().ChangeBullet(iBulletIndex);
        iBulletIndex++;
        if(iBulletIndex>=3)
        {
            iBulletIndex = 0;
        }
    }
}
