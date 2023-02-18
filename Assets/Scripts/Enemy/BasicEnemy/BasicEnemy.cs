using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {
    // Start is called before the first frame update

    public List<Vector3> _path;
    private float movementSpeed = 1f;
    private uint damageToNexus = 200;

    private Vector3 FinalNode;

    public void Start() {
        var basicEnemy = this.gameObject;
        var enemyNode = basicEnemy.transform.parent.gameObject;
        var pathNode = enemyNode.transform.parent.gameObject;
        var waypointPathNode = pathNode.transform.GetChild(0).gameObject;

        _path = new List<Vector3>(waypointPathNode.transform.GetComponent<WaypointPath>().GetPath());
        
        this.gameObject.transform.position = _path.First();
        FinalNode = _path.Last();

        this.gameObject.transform.GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    public void Update() {
        if (_path.Count > 0) {
            MoveOnPath(); 
        } else {
            AttackNexus();
        }
        
    }

    private void MoveOnPath() {
        transform.position = Vector3.MoveTowards(transform.position, _path.First(), movementSpeed * Time.deltaTime);
        if (transform.position.Equals(_path.First())) {
            _path.RemoveAt(0);
        }
    }

    private void AttackNexus() {
        //TODO: Deal damage to nexus
    }
}
