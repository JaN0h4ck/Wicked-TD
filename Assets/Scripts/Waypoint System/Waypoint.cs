using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour {
    private void OnDrawGizmos() {
        Gizmos.DrawSphere(this.gameObject.transform.position, 0.1f);
    }
}
