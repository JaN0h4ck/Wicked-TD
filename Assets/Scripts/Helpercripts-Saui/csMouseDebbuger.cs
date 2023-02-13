using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Bindings;
using System.Runtime.InteropServices;

public class csMouseDebbuger : MonoBehaviour
{
 

    private int iFps;

    public bool bDebugFps;

    int iObjectCount;

    [Tooltip("Migth lag sometimes, because this command isnt optimized ")]
    public bool bGetObjectsInScene;

    [SerializeField]
    private int iFpsSamples = 5;
   
    void Start()
    {
        StartCoroutine(UpdateFps());
    }
    private void OnGUI()
    {
        //MousePos: Links unten ist 0/0
        string sOutput="Mouse: ";
        sOutput += Input.mousePosition.ToString();
       
        sOutput += "\nfps:  " + iFps;
        if (bGetObjectsInScene == true)
        {
            var foundObjects = Object.FindObjectsOfType<GameObject>();
            int count = foundObjects.Length;
            sOutput += "\n Total Objects in scene: " + iObjectCount;
        }
        GUI.Label(new Rect(40, 20, 300, 80), sOutput);
    }

    private IEnumerator UpdateFps()
    {
        float fFpsAverage = 0;
        int iCounter = 0;
        while (true)
        {
            //iFps = (int)(1f / Time.unscaledDeltaTime);
            fFpsAverage += 1f / Time.unscaledDeltaTime;
            if (bDebugFps==true)
            {
                Debug.Log("(Debug): Fps-Count = " + iFps);
            }
            if (bGetObjectsInScene == true)
            {
                var foundObjects = Object.FindObjectsOfType<GameObject>();
               iObjectCount = foundObjects.Length;
            }
            float f = 1 / iFpsSamples;
            yield return new WaitForSecondsRealtime(f);
           
            if(iCounter>iFpsSamples)
            {
                iFps = (int)(fFpsAverage / iFpsSamples);
                iCounter = 0;
                fFpsAverage = 0;
                
            }
            iCounter++;
        }
    }

 
}
