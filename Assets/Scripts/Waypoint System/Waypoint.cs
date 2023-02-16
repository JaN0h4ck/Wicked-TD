using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Vector3 hateCrime;
    public void Start()
    {
        hateCrime = this.gameObject.transform.position;
    }
}
