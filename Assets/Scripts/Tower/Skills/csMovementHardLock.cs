using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMovementHardLock : MonoBehaviour
{
    private Vector3 v3PositionToLock;
    private float fDuration;
    
    void Update()
    {
        if(this.transform.hasChanged)
        {
            this.transform.position = v3PositionToLock;
        }
    }

    public void AddTimer(float fTime)
    {        
        fTime = fDuration;
        Invoke("StopMovementLock", fDuration);
    }

    private void StopMovementLock()
    {
        Destroy(this);
    }
}
