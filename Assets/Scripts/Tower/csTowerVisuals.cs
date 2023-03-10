using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTowerVisuals : MonoBehaviour
{
    #region Variables

    [SerializeField]
    [Tooltip("Put in here the different sprites for the tower visual progression.!!NOTE!!:The baselook for each tower is set DIRECTLY in the towers prefab in its spritrenderer. This script is ONLY for the towers evolution sprites 0=okay tower mode, 1= less good ,2 = current max and worst.")]
    private List<Sprite> splTowerLooks;

    //Border iTime has to rach to updte to a new prite 1Sec = 10 iTime
    public static int iTimeTreshold = 100;

    private csTowerBaseScript TowerScript;

    private int iTime = 0;

    private int iSpriteIndex = 0;

    private SpriteRenderer sprTowerRenderer;

    private bool bFinalStageReached = false;

    private int iFaktor = 10;

    #endregion

    /*
     * Important:
     * The start-sprite is directly set in the prefabs spriterenderer
     * This is only used for its evolutions
     */
    #region Setup
    private void Start()
    {
        TowerScript = gameObject.GetComponent<csTowerBaseScript>();
        Invoke("Setup", 0.02f);
    }

    private void Setup()
    {
        sprTowerRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(UpdateViuals());
    }
    #endregion

    #region visualUpdate
    private IEnumerator UpdateViuals()
    {
        while(bFinalStageReached == false)
        {
            if (DidTowerEnterNewProgressionState())
            {
                Debug.LogWarning("Updating");
                UpdateToNextSprite();
            }
            iTime++;
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    private void UpdateToNextSprite()
    {
        if (iSpriteIndex < splTowerLooks.Count)
        {
            sprTowerRenderer.sprite = splTowerLooks[iSpriteIndex];
            iSpriteIndex++;
            gameObject.GetComponent<csWeapon>().UpdateAnimationTimeProgressionToNextStage();
            gameObject.GetComponent<csWeapon>().PlayIdleAnimation();
        }
    }
    #endregion

    #region SpeedChecks
    private bool DidTowerEnterNewProgressionState()
    {
        TowerScript.PullCurrency();
       for(int i=0; i< TowerScript.GetPoints().Length; i++)
       {
            if(TowerScript.GetPoints()[i]>iFaktor)
            {
                iFaktor *= 2;
                return true;
            }
       }
            return false;
     }
   

    private float GetCurrentFireSpeed()
    {
        return TowerScript.GetFireSpeed();
    }
    #endregion
}
