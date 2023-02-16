using System.Collections.Generic;
using UnityEngine;


public class WaypointPath : MonoBehaviour {
    // Start is called before the first frame update
    
    public List<Vector3> path;
    public void Awake() {
        for (int i = 0; i < this.gameObject.transform.childCount; i++) {
            path.Add(this.gameObject.transform.GetChild(i).transform.position);
        }
    }
    
    
    public List<Vector3> GetPath() {
        return path;
    }
}
