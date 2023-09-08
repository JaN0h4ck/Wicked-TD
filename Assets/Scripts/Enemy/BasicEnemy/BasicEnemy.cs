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
    private float baseMovementSpeed = 1.0f;
    
    protected Nexus nexusLogic;
    protected SpriteRenderer spriteRenderer;
    

    public void Start() {
        var basicEnemy = this.gameObject;
        var enemyNode = basicEnemy.transform.parent.gameObject;
        var pathNode = enemyNode.transform.parent.gameObject;
        var waypointPathNode = pathNode.transform.GetChild(0).gameObject;
        var mapNode = pathNode.transform.parent.gameObject;
        var nexusNode = mapNode.transform.GetChild(0).gameObject;

        nexusLogic = nexusNode.GetComponent<Nexus>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        
        _path = new List<Vector3>(waypointPathNode.transform.GetComponent<WaypointPath>().GetPath());
        
        this.gameObject.transform.position = _path.First();
        FinalNode = _path.Last();
        spriteRenderer.flipX = !h_calculateFaceDirection();

        this.gameObject.transform.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Update() {
        //what could go wrong?
        var nexusNode = this.gameObject
            .transform.parent.gameObject
            .transform.parent.gameObject
            .transform.GetChild(0).gameObject
            .transform.parent.gameObject
            .transform.GetChild(0).gameObject;
        
        if (_path.Count > 0) {
            MoveOnPath();
        } else {
            AttackNexusAndDeconstruct();
        }
        
        if (!nexusLogic.alive)
        {
            Destroy(this);
        }
            
    }
    
    private void AttackNexusAndDeconstruct()
    {
        var basicEnemy = this.gameObject;
        var enemyNode = basicEnemy.transform.parent.gameObject;
        var pathNode = enemyNode.transform.parent.gameObject;
        var mapNode = pathNode.transform.parent.gameObject;
        var nexusNode = mapNode.transform.GetChild(0).gameObject;
        
        nexusLogic.LooseHealth(damageToNexus);
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
        transform.position = Vector3.MoveTowards(this.gameObject.transform.position, _path.First(), baseMovementSpeed * movementSpeed * Time.deltaTime);;
        if (InRange(this.gameObject.transform.position, _path.First())) {
            _path.RemoveAt(0);
            spriteRenderer.flipX = !h_calculateFaceDirection(); //DO NOT TOUCH!!
        }
    }

    protected bool h_calculateFaceDirection()
    {
        if (_path.Count <= 0)   // Waypoints are removed during movement. Prevents a invalid operation.
            return true;
        if (h_InRange(transform.position.y, _path.First().y, 0.02f))
        {
            return transform.position.x > _path[1].x;
        }
        return transform.position.x > _path.First().x;
    }

    private bool InRange(Vector3 a, Vector3 b)
    {
        if (h_InRange(a.x , b.x) && h_InRange(a.y , b.y) && h_InRange(a.z , b.z))
            return true;
        return false;
    }

    private bool h_InRange(float a, float b, float rangeToWaypoint = 0.01f)
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
