using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csIndicatorAnimation : MonoBehaviour
{
    #region  Variables
    [SerializeField]
    private float fSpeed;
    [SerializeField]
    private float fScaleSpeed;
    #endregion

    void Start()
    {
        StartCoroutine(FlyUpWardAnimation());
    }


    private IEnumerator FlyUpWardAnimation()
    {
        int i=0;
        fSpeed = Random.Range(fSpeed/2,fSpeed);
        float fSpeedX = Random.Range(-fSpeed/2 , fSpeed/2);
        while (i<100)
        {
            this.transform.position = new Vector2(this.transform.position.x+fSpeedX*Time.deltaTime, this.transform.position.y + fSpeed*Time.deltaTime);
            i++;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        StartCoroutine(PopAnimation());
    }

    private IEnumerator PopAnimation()
    {
        int i = 0;
        while (i < 100)
        {
            this.transform.localScale = new Vector2(this.transform.localScale.x + fScaleSpeed * Time.deltaTime, this.transform.localScale.y + fScaleSpeed * Time.deltaTime);
            i++;
            yield return new WaitForSecondsRealtime(0.0025f);
        }
        Destroy(this.gameObject);
    }
}