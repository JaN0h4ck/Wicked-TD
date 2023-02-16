using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Vector3> _path;
    public GameObject pathObject;

    public void Start()
    {
        var basicEnemy = this.gameObject;
        var enemyNode = basicEnemy.transform.parent.gameObject;
        var pathNode = enemyNode.transform.parent.gameObject;
        var waypointPathNode = pathNode.transform.GetChild(0).gameObject;
        Debug.Log(waypointPathNode.GetComponent<WaypointPath>().name);

        WaypointPath test = waypointPathNode.transform.GetComponent<WaypointPath>();
        
        _path = new List<Vector3>(test.GetPath());

        foreach (var node in _path)
        {
            Debug.Log("At Enemy: " + node);
        }
        Debug.Log("Test");
        foreach (var node in pathObject.GetComponent<WaypointPath>().path)
        {
            Debug.Log("Test");
            Debug.Log("At Enemy2: " + node);
        }
        
        foreach (var node in pathObject.GetComponent<WaypointPath>().GetPath())
        {
            Debug.Log("Test");
            Debug.Log("At Enemy2: " + node);
        }

        //this.gameObject.transform.position = _path.First();


    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    private void MoveOnPath()
    {
        
    }
}
