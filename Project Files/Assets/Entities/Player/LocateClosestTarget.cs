using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateClosestTarget : MonoBehaviour {
    public Transform[] targets;
    public Transform referencePoint = null;
    public float rotatingSpeed = 160f;
    public Transform nearest;
    public Transform finalDestination;
    Quaternion targetRot;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        nearest = ClosestTarget();
        if (nearest==null)
        {
            nearest = finalDestination;
        }
        targetRot = Quaternion.LookRotation(transform.forward, nearest.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotatingSpeed * Time.deltaTime);
	}
    Transform ClosestTarget()
    {
        Transform closest = null;
        float closestDist = float.MaxValue;
        foreach (Transform tra in targets)
        {
            if (tra)
            {
                float dist = Vector3.Distance(tra.position, referencePoint.position);
                if (dist <= closestDist)
                {
                    closestDist = dist;
                    closest = tra;
                }
            }
        }
        return closest;
    }
}