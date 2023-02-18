using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPosionDamage : MonoBehaviour
{
    private float fDamage;
    private float fDuration;
    private csDamageManager DamageIndicator;
    public void StartEffect()
    {
        DamageIndicator = csDamageManager.current;
        Invoke("StopEffect", fDuration);
        StartCoroutine(DoDOT());
    }
    public void SetDamageOverTimeAndDuration(float fDOT, float fDamageDuration)
    {
        fDamage = fDOT;
        fDuration = fDamageDuration;
    }

    private IEnumerator DoDOT()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(2f);
            this.gameObject.GetComponent<csEnemyHealth>().LooseHealth(fDamage);
            DamageIndicator.ShowDamageAt(transform, fDamage);
        }
    }

    private void StopEffect()
    {
        Destroy(this);
    }
}
