using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WaypointPath : MonoBehaviour
{
    // Start is called before the first frame update
    
    public List<Vector3> path;
    public void Start()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            path.Add(this.gameObject.transform.GetChild(i).transform.position);
        }

        foreach (var childSprite in this.gameObject.transform.GetComponentsInChildren<SpriteRenderer>()) {
            childSprite.enabled = false;
        }
        
        //Debug.Log(path);
        foreach (var node in path)
        {
            Debug.Log("Waypoint Path: " + node);
        }
    }
    
    
    public List<Vector3> GetPath() {
        return path;
    }
}
