using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csFreezeSkill : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float fDuration;

    [SerializeField]
    private Vector2 v2Size;

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
    }


    private void FreezeEnemiesInRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(this.transform.position, v2Size,  0.0f, ShootScript.GetEnemieLayer());
        List<Transform> tslEnemies = new List<Transform>();
        foreach (Collider2D cd in hitColliders)
        {
            csMovementHardLock temp =  cd.gameObject.AddComponent <csMovementHardLock> ();
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
