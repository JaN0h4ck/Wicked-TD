using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMovementHardLock : MonoBehaviour
{
    private Vector3 v3PositionToLock;
    private float fDuration;

    private void Start()
    {
        v3PositionToLock = this.transform.position;
    }
    void Update()
    {
        if(this.transform.hasChanged)
        {
            this.transform.position = v3PositionToLock;
        }
    }

    public void AddTimer(float fTime)
    {        
        fDuration = fTime;
        Debug.LogWarning("!S´tatring" +fDuration);
        Invoke("StopMovementLock", fDuration);
    }

    private void StopMovementLock()
    {
        Debug.LogWarning("!ending" + fDuration);
        Destroy(this);
    }
}
