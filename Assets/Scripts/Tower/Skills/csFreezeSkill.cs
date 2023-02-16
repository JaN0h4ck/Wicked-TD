using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csFreezeSkill : csSkillBaseScript
{
    #region Variables

    private csWeapon ShootScript;
    private GameObject gMainObject;

    private List<SpriteRenderer> sprlRenderers;
    private List<Color> crlSavepriteColors;
    #endregion
    private void Start()
    {
        sprlRenderers = new List<SpriteRenderer>();
        crlSavepriteColors = new List<Color>();
        gMainObject = this.transform.parent.gameObject;
        ShootScript = gMainObject.GetComponent<csWeapon>();
        FreezeEnemiesInRange();
        Invoke("UnfreezeAnimation", fDuration);
        DestroyTimer();
    }


    private void FreezeEnemiesInRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(this.transform.position, v2Size,  0.0f, ShootScript.GetEnemieLayer());
        foreach (Collider2D cd in hitColliders)
        {
            csMovementHardLock temp =  cd.gameObject.AddComponent<csMovementHardLock>();
            Debug.LogWarning(cd.GetComponent<csMovementHardLock>());
            temp.AddTimer(fDuration);
            SetupFreezeAnimation(cd.gameObject);
        }
        FreezeAnimation();
    }

    #region Animation
    private void SetupFreezeAnimation(GameObject gTarget)
    {
        sprlRenderers.Add(gTarget.GetComponent<SpriteRenderer>());
        crlSavepriteColors.Add(gTarget.GetComponent<SpriteRenderer>().color);
    }

    private void FreezeAnimation()
    {
        foreach(SpriteRenderer spTemp in sprlRenderers)
        {
            spTemp.color = new Color(0.55f,0.86f,0.9f,0.7f);
        }
    }

    private void UnfreezeAnimation()
    {
        int i = 0;
        foreach (SpriteRenderer spTemp in sprlRenderers)
        {
            spTemp.color = crlSavepriteColors[i];
            i++;
        }
    }
    #endregion

   

}
