using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a simple script that draws a gizmo for it's location (which i will use as waypoints)

public class Waypoint : MonoBehaviour {
    [SerializeField] protected float debugDrawRadius = 1.0F;
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius); ;
    }

}
