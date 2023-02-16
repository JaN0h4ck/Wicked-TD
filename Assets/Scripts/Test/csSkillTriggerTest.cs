using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSkillTriggerTest : MonoBehaviour
{
    public bool bActivate = false;
    private bool bCompare = false;
    public GameObject gTowerToActivate;
    public int iSkillIndex;

    void Update()
    {
        if(bActivate != bCompare)
        {
            bCompare = bActivate;
            TriggerSkill();
        }
    }

    private void TriggerSkill()
    {
        gTowerToActivate.GetComponent<csTowerBaseScript>().ActivateSkill(iSkillIndex);
    }
}
