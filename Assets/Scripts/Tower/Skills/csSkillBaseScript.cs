using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSkillBaseScript : MonoBehaviour
{
    #region Variables

    [SerializeField]
    protected float fDuration;

    [SerializeField]
    protected Vector2 v2Size;

    #endregion
    protected void DetroySkill()
    {
        Destroy(gameObject);
    }

    protected void DestroyTimer()
    {
        Invoke("DestroySkill", fDuration);
    }
}
