using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour {

    [SerializeField] float dieAFter = 5;
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject,dieAFter);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
