using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSirenAnimation : MonoBehaviour
{
    #region variables
    [SerializeField]
    private float fSirenSpeed;

    private bool bEnabled = false;
#endregion
    private void OnEnable()
    {
        bEnabled = true;
    }
    private void OnDisable()
    {
        bEnabled = false;
    }

    private void Update()
    {
        if (bEnabled)
        {
            this.transform.Rotate(0, 0, fSirenSpeed*Time.deltaTime);
        }
    }
}
