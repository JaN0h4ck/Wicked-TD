using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WayPointPathDrawGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        for (int iterator = 0; iterator < this.gameObject.transform.childCount - 1; iterator++) {
            Vector3 origin = this.gameObject.transform.GetChild(iterator).transform.position;
            Vector3 destination = this.gameObject.transform.GetChild(iterator + 1).transform.position;
            Gizmos.DrawLine(origin, destination);
        }
    }
}
