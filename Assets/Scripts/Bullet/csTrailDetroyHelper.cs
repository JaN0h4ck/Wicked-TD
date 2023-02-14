using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTrailDetroyHelper : MonoBehaviour
{
    /*
     * This is only for bullets which trails need to be destroyed delayed
     */

    [SerializeField]
    private float fDetroyDelay;

    private void Start()
    {
        Invoke("DestroyTrail", fDetroyDelay);   
    }

    private void DestroyTrail()
    {
        Destroy(this.gameObject);
    }
}
