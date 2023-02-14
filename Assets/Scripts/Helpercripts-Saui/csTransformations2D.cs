using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTransformations2D : MonoBehaviour
{
    /*
     * Thi script is used as a transformation-helper
     * it provides some Usefull 2D Tranformation-Functions, like look at2d
     */
    #region Variables
    [SerializeField]
    private float fDrag;

    private List<Transform> tslMoveTowardObjects;
    #endregion
    #region Setup
    private void Awake()
    {
        tslMoveTowardObjects = new List<Transform>();
    }
    #endregion

    /// <summary>
    /// Rotates the object toward a target object
    /// </summary>
    /// <param name="tsObject"></param>
    /// <param name="tsTarget"></param>
    public void LookAt2D(Transform tsObject, Transform tsTarget)
    {
        Vector3 dir = tsTarget.position - tsObject.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        tsObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        tsObject.transform.Rotate(0,0,90);
    }

    /// <summary>
    /// This function just flips the sprite by scale
    /// The bool X marks wether the sprite will by flipped along X or Y-Axis
    /// </summary>
    /// <param name="tsObject"></param>
    /// <param name="bAlongX"></param>
    public void Flip(Transform tsObject, bool bAlongX = true)
    {
        Vector3 v3Temp = tsObject.transform.localScale;
        if(bAlongX)
        {
            v3Temp.x *= -1;
        }
        else
        {
            v3Temp.y *= -1;
        }
        tsObject.transform.localScale = v3Temp;
    }

    /// <summary>
    /// Returns the distance between two objects
    /// </summary>
    /// <param name="tsA"></param>
    /// <param name="tsB"></param>
    /// <returns></returns>
    public float GetDistance(Transform tsA, Transform tsB)
    {
        return Vector3.Distance(tsA.position, tsB.position);
    }

    public void MoveTowardsLoop(Transform tsObject, Transform tsTarget,float fSpeed, float fTime=0)
    {
        tslMoveTowardObjects.Add(tsObject);
        StartCoroutine(MoveObjectTowards(tsObject, tsTarget, fSpeed, fTime));
    }

    private IEnumerator MoveObjectTowards(Transform tsObject, Transform tsTarget, float fSpeed, float fTime)
    {
        fTime *= 100;
        if(fTime==0)
        {
            fTime = -1;
        }
        while(tslMoveTowardObjects.Contains(tsObject)&&fTime!=0&&tsObject!=null)
        {
            tsObject.position = Vector3.MoveTowards(tsObject.position, tsTarget.position, fSpeed * Time.deltaTime);
            if(fTime>0)
            {
                fTime--;
            }
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Debug.LogError("ending");
    }

    public void StopMoveTowardsLoop(Transform tsObject)
    {
        if (tslMoveTowardObjects.Contains(tsObject))
        {
            tslMoveTowardObjects.Remove(tsObject);
        }
    }

    #region Physics
    /// <summary>
    /// Pushes the given object withforce
    /// If the object doent hve a rigidbody one will be added
    /// </summary>
    public void Push(GameObject gObject, Vector3 v3Direction, float fForce)
    {
        if(gObject.GetComponent<Rigidbody2D>()==null)
        {
            Rigidbody2D rb = gObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.drag =fDrag;
        }
        if (gObject.GetComponent<BoxCollider2D>() == null&& gObject.GetComponent<CircleCollider2D>() == null && gObject.GetComponent<CapsuleCollider2D>() == null)
        {
           // gObject.AddComponent<BoxCollider2D>();
        }

        v3Direction *= fForce;
        gObject.GetComponent<Rigidbody2D>().AddForce(v3Direction, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Pushes the given object with force angular
    /// If the object doent hve a rigidbody one will be added
    /// </summary>
    public void PushAngular(GameObject gObject,float fForce)
    {
        if (gObject.GetComponent<Rigidbody2D>() == null)
        {
            Rigidbody2D rb = gObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.drag = fDrag;
        }

        gObject.GetComponent<Rigidbody2D>().AddTorque(fForce);
    }

    /// <summary>
    /// Pushes the given object continuasly with force
    /// If the object doent hve a rigidbody one will be added
    /// </summary>
    public void PushForce(GameObject gObject, Vector3 v3Direction, float fForce)
    {
        if (gObject.GetComponent<Rigidbody2D>() == null)
        {
           Rigidbody2D rb= gObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.drag = fDrag;
        }
        if (gObject.GetComponent<BoxCollider2D>() == null && gObject.GetComponent<CircleCollider2D>() == null && gObject.GetComponent<CapsuleCollider2D>() == null)
        {
            gObject.AddComponent<BoxCollider2D>();
        }

        v3Direction *= fForce;
        gObject.GetComponent<Rigidbody2D>().AddForce(v3Direction, ForceMode2D.Force);
    }


    #endregion
}