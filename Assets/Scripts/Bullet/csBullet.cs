using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csBullet : MonoBehaviour
{
    #region Variables
    private csTransformations2D Transformation;

    private ParticleSystem psTrailEffect;
    #endregion

    #region Setup
    /// <summary>
    /// Define the object at which the bullet is shot
    /// </summary>
    public void ShootAt(Transform tsTarget, float fBulletSpeed) {
        Transformation = gameObject.GetComponent<csTransformations2D>();
        if (Transformation != null) {
            Transformation.MoveTowardsLoop(this.transform, tsTarget, fBulletSpeed);
            StartCoroutine(WaitTillTargetHit(tsTarget));
        } else {
            Debug.LogError("Your bullet is etup incorrectly. Its missing a csTransformation2D script - called by " + this.gameObject);
        }
        if(this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>()!=null)
        {
            psTrailEffect = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            psTrailEffect.Play();
        }
        else
        {
            Debug.LogWarning("Your bullet migth be setup incorrectly. Its missing a Trail Particle System - called by " + this.gameObject);
        }
    }
    #endregion

    #region FlyBehaviour
    private IEnumerator WaitTillTargetHit(Transform tsTarget) {
        bool isRunning = true;
        while(isRunning) {
            Transformation.LookAt2D(this.transform, tsTarget);
            yield return new WaitForSecondsRealtime(0.05f);
            
            if(WasTargetHit(tsTarget)) {
                isRunning = false;
            }
        }
        Debug.Log("+(Bullet): Hit target: " + tsTarget.name);
        Transformation.StopMoveTowardsLoop(this.transform);
        Destroy(this.gameObject);
    }

    private bool WasTargetHit(Transform tsTarget) {
        if(tsTarget.position==this.transform.position) {
            return true;
        } else {
            return false;
        }
    }
    #endregion
}
