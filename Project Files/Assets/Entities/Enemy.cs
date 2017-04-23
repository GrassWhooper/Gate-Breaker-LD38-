using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] float chaseRange = 5.0f;
    [SerializeField] float attackRange = 3.0f;
    [SerializeField] float turnSpeed = 160f;
    Boid boid;
    Health health;
    Transform target;
    float dist;
    Attack attack;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void Start ()
    {
        boid = GetComponent<Boid>();
        health = GetComponent<Health>();
        health.OnDeathEvent += Health_OnDeathEvent;
        target = GameObject.FindGameObjectWithTag(Constants.PlayerTag_KEY).transform;
        attack = GetComponent<Attack>();
	}

    private void Health_OnDeathEvent()
    {
        if (boid)
        {
            boid.boidController.RemoveBoid(boid);
        }
    }
    private void LateUpdate()
    {
        Vector3 p = transform.position;
        p.z = 0;
        transform.position = p;
        attack.StopAttack();
        if (target==null)
        {
            target = GameObject.FindGameObjectWithTag(Constants.PlayerTag_KEY).transform;
        }
        dist = Vector3.Distance(target.position, transform.position);
        if (dist<=chaseRange)
        {
            if (boid)
            {
                boid.Target = target;
            }
        }
        if (dist>chaseRange)
        {
            if (boid)
            {
                boid.enabled = true;
                target = null;
                boid.Target = target;
            }
        }
        if (dist <= attackRange)
        {
            if (boid)
            {
                boid.enabled = false;
            }
            LookAt();
            attack.StartAttack();
        }
    }
    void LookAt()
    {
        if (target==null)
        {
            target= GameObject.FindGameObjectWithTag(Constants.PlayerTag_KEY).transform;
        }
        if (target!=null)
        {
            Quaternion targetRot = Quaternion.LookRotation(transform.forward, -transform.position + target.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
        }
    }
}