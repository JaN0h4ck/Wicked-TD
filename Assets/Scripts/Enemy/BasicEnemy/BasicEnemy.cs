using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class BasicEnemy : csEnemyHealth {
    // Start is called before the first frame update

    public List<Vector3> _path;
    protected float movementSpeed = 1f;
    protected float damageToNexus = 2f;
    
    private Vector3 FinalNode;
    private float rangeToWaypoint = 0.001f;
    

    public void Start() {
        var basicEnemy = this.gameObject;
        var enemyNode = basicEnemy.transform.parent.gameObject;
        var pathNode = enemyNode.transform.parent.gameObject;
        var waypointPathNode = pathNode.transform.GetChild(0).gameObject;
        var mapNode = pathNode.transform.parent.gameObject;
        var nexusNode = mapNode.transform.GetChild(0).gameObject;
        
        
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
            AttackNexusAndDeconstruct();
        }
    }
    
    private void AttackNexusAndDeconstruct()
    {
        var basicEnemy = this.gameObject;
        var enemyNode = basicEnemy.transform.parent.gameObject;
        var pathNode = enemyNode.transform.parent.gameObject;
        var mapNode = pathNode.transform.parent.gameObject;
        var nexusNode = mapNode.transform.GetChild(0).gameObject;
        
        nexusNode.transform.GetComponent<Nexus>().LooseHealth(damageToNexus);
        LooseHealth(fHealth);
    }

    public override void LooseHealth(float fDamage)
    {
        fHealth -= fDamage;
        CheckForDeath();
    }

    protected override void CheckForDeath()
    {
        if(fHealth<=0)
        {
            //TODO: Death Animation
            Destroy(this.gameObject);
        }
    }
    
    protected virtual void MoveOnPath()
    {
        transform.position = Vector3.MoveTowards(this.gameObject.transform.position, _path.First(), movementSpeed * Time.deltaTime);
        if (InRange(this.gameObject.transform.position, _path.First())) {
            _path.RemoveAt(0);
            Debug.Log( gameObject.transform.name + " [" + this.gameObject.transform.parent.gameObject.transform.parent.gameObject.name + "] " + _path.Count);
        }
    }

    private bool InRange(Vector3 a, Vector3 b)
    {
        if (h_InRange(a.x , b.x) && h_InRange(a.y , b.y) && h_InRange(a.z , b.z))
            return true;
        return false;
    }

    private bool h_InRange(float a, float b)
    {
        float rangeBetween;
        if (b > a)
            (a, b) = (b, a);
        rangeBetween = a - b;
        if (rangeBetween <= rangeToWaypoint)
            return true;
        return false;
    }
    
}
