using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boid : MonoBehaviour
{
    public Transform[] targets = new Transform[2];
    public float reachedTargetRadious = 0.8f;
    public Transform Target{get { return target; }set{target = value;if (value == null){target = targets[indexOfTarget];}}}
    public Vector3 Velocity { get { return velocity; } }
    public Vector3 MovementDirection { get { return movementDirection; } }
    public BoidsController boidController { get { return boidsController; } }
    bool followsMouse = false;
    BoidsController boidsController;
    Vector3 movementDirection;
    public List<Boid> negibours = new List<Boid>();
    public List<Boid> collidedBoids = new List<Boid>();
    [SerializeField] Transform target = null;
    int indexOfTarget = 0;
    Vector3 BoidToTargetDir
    {
        get
        {
            Vector3 boidToTarget = new Vector3();
            if (followsMouse == false)
            {
                boidToTarget = (target.position - transform.position).normalized;
            }
            return boidToTarget;
        }
    }
    Vector3 lastPosition = new Vector3();
    Vector3 velocity = new Vector3();
    Vector3 targetVelocity = new Vector3();
    Vector3 dampVelocity = new Vector3();
    int lastTargetIndex = 0;
    Boid GetClosestBoid(List<Boid> boids)
    {
        float closestDist = float.MaxValue;
        Boid closestBoid = null;
        foreach (Boid boid in boids)
        {
            float dist = Vector3.Distance(transform.position, boid.transform.position);
            if (closestDist >= dist)
            {
                closestDist = dist;
                closestBoid = boid;
            }
        }
        return closestBoid;
    }

    float AwayFromColPercent
    {
        get
        {
            Vector3 avgToBoid = collidedBoids.GetAveragePosition() - transform.position;
            float div = avgToBoid.magnitude;
            if (div==0)
            {
                div=1;
            }
     
            float percent = boidsController.CollisionRadious / div;
            return percent;
        }
    }
    Vector3 AwayFromCollAvgDir
    {
        get
        {
            Vector3 averagePosition = collidedBoids.GetAveragePosition();
            Vector3 avgPosToBoid = (-averagePosition + transform.position).normalized;
            return avgPosToBoid;
        }
    }
    Vector3 AvgNegiboursPoslDir
    {
        get
        {
            Vector3 avg = negibours.GetAveragePosition();
            Vector3 boidToAvgCenter = (avg - transform.position).normalized;
            return boidToAvgCenter;
        }
    }
    Vector3 AvgGroupVel
    {
        get
        {
            Vector3 avg = new Vector3();
            foreach (Boid boid in negibours)
            {
                avg += boid.Velocity;
            }
            avg = avg / negibours.Count;
            return avg.normalized;
        }
    }

    // Use this for initialization
    void Start()
    {

        target = targets[indexOfTarget];
        negibours = boidsController.Boids;
        movementDirection = BoidToTargetDir;
        lastPosition = transform.position;
        targetVelocity = transform.position;
        
    }
    void SetTaret()
    {
        
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist<=reachedTargetRadious)
        {
            indexOfTarget++;
            if (indexOfTarget>=targets.Length)
            {
                indexOfTarget = 0;
            }
        }
        if (lastTargetIndex!= indexOfTarget)
        {
            lastTargetIndex = indexOfTarget;
            target = targets[indexOfTarget];
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        SetTaret();
        GetCollision();

        targetVelocity = BoidToTargetDir * boidsController.TargetAttractionM +
            AwayFromCollAvgDir * boidsController.CollisionAvoidanceM * AwayFromColPercent +
            AvgNegiboursPoslDir * boidsController.FlockCenteringM +
            AvgGroupVel * boidsController.GroupVelMatchingM;
        targetVelocity.Normalize();
        targetVelocity = targetVelocity * boidsController.Speed;

        velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref dampVelocity, boidsController.SmoothTime);

        Debug.DrawRay(transform.position, AvgNegiboursPoslDir, Color.red);

        transform.position += velocity * Time.deltaTime;

        if (boidsController.LookAtTarget)
        {
            movementDirection = BoidToTargetDir;
        }
        else
        {
            movementDirection = (transform.position - lastPosition).normalized;
        }


        lastPosition = transform.position;
        if (boidsController.RotateTowardsMovementDirection)
        {
            Quaternion rotation = Quaternion.LookRotation(transform.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 180.0f * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (boidsController == null)
        {
            boidsController = FindObjectOfType<BoidsController>();
        }
        if (boidsController == null)
        {
            return;
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, boidsController.CollisionRadious);
    }

    public void TakeInitials(BoidsController boidsController)
    {
        this.boidsController = boidsController;
    }

    void GetCollision()
    {
        foreach (Boid boid in negibours)
        {
            if (boid != null)
            {
                float dist = Vector3.Distance(boid.transform.position, transform.position);
                if (dist <= boidsController.CollisionRadious)
                {
                    if (collidedBoids.Contains(boid) == false)
                    {
                        collidedBoids.Add(boid);
                    }
                }
                else if (dist > boidsController.CollisionRadious)
                {
                    if (collidedBoids.Contains(boid) == true)
                    {
                        collidedBoids.Remove(boid);
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        //Boid b = collider.GetComponent<Boid>();
        //if (b != null && negibours.Contains(b) == false)
        //{
        //    negibours.Add(b);
        //}
    }

    private void OnTriggerExit(Collider collider)
    {
        //Boid b = collider.GetComponent<Boid>();
        //if (b != null && negibours.Contains(b))
        //{
        //    negibours.Remove(b);
        //}
    }
}
