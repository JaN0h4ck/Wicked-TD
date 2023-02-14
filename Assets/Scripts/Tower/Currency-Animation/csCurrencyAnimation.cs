using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCurrencyAnimation : MonoBehaviour
{
    /*
     * This is used as a physics based animation if a tower is destroyed
     * It will drop some gold, to indicate the player cot the recources
     */
    #region Variables
    private csTransformations2D Transformation;

    [SerializeField]
    [Tooltip("The force will be a random value between x and y")]
    private Vector2 v2RandomForce;

    [SerializeField]
    private float fLifetime;
    #endregion
    private void Start()
    {
        Transformation = this.gameObject.GetComponent<csTransformations2D>();
        AddRandomForce();
        Invoke("StartFade", fLifetime);
    }

    /// <summary>
    /// Adds a random force to the currency object -->physic based animation
    /// </summary>
    private void AddRandomForce()
    {
        Transformation.Push(this.gameObject,GetRandomDirection(),GetRandomForce());
        Transformation.PushAngular(this.gameObject,GetRandomForce()*100);
    }

    #region Random
    private Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f),0);
    }

    private float GetRandomForce()
    {
        return Random.Range(v2RandomForce.x,v2RandomForce.y);
    }
    #endregion

    #region Fade

    private void StartFade()
    {
        StartCoroutine(FadeAnimation());
    }

    private IEnumerator FadeAnimation()
    {
        SpriteRenderer sprCurrency = this.gameObject.GetComponent<SpriteRenderer>();
        Color clSpriteColor;
        int iCounter = 100;
        while(iCounter>0)
        {
            clSpriteColor = sprCurrency.color;
            clSpriteColor = new Color(clSpriteColor.r, clSpriteColor.g, clSpriteColor.b, clSpriteColor.a - 0.01f);
            sprCurrency.color = clSpriteColor;
            iCounter--;
            yield return new WaitForSecondsRealtime(0.01f);

        }
        Destroy(this.gameObject);
    }


    #endregion
}

