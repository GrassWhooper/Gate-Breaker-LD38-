using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float chaseSpeed = 5;
    Vector3 offSet;
	// Use this for initialization
	void Start ()
    {
        offSet = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offSet,chaseSpeed*Time.deltaTime);
    }
}