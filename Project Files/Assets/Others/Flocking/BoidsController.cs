using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoidsController : MonoBehaviour
{

    public const string boidTag = "Boid";

    public float CollisionRadious { get { return collisionRadious; } }
    public float FlockCenteringM { get { return flockCenteringM; } }
    public float CollisionAvoidanceM { get { return collisionAvoidanceM; } }
    public float GroupVelMatchingM { get { return groupVelMatchingM; } }
    public float TargetAttractionM { get { return targetAttractionM; } }
    public float SmoothTime { get { return smoothTime; } }
    public Negibouring NegibouringMethod { get { return negibouringMethod; } }
    public bool RotateTowardsMovementDirection { get { return rotateTowardsMovementDirection; } }
    public bool LookAtTarget { get { return lookAtTarget; } }
    public float Speed{get{return speed;}}
    public List<Boid> Boids{get{return boids;}}

    [Tooltip("when false it will look at the movement direction if true it will look at the target")]
    [SerializeField]
    bool lookAtTarget = false;

    [Header("Initials")]
    [SerializeField] float collisionRadious = 1;
    [Tooltip("Time required for each boid to reach its target velocity")]
    [SerializeField]
    float smoothTime = 0.250f;
    bool rotateTowardsMovementDirection = true;

    [Header("Priorities Of Behavior")]
    [SerializeField] float flockCenteringM = 0.1f;
    [SerializeField] float collisionAvoidanceM = 0.6f;
    [SerializeField] float groupVelMatchingM = 0.1f;
    [SerializeField] float targetAttractionM = 0.3f;
    [SerializeField] List<Boid> boids = new List<Boid>();
    [SerializeField] float speed = 4.0f;

    public enum Negibouring { ThroughParent, ThroughCollision }
    Negibouring negibouringMethod = Negibouring.ThroughParent;
    public void RemoveBoid(Boid boidToRemove)
    {
        boids.Remove(boidToRemove);
        foreach (Boid b in boids)
        {
            b.negibours.Remove(boidToRemove);
        }
    }
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform tra = transform.GetChild(i);
            Boid b = tra.GetComponent<Boid>();
            if (b != null)
            {
                boids.Add(b);
                b.TakeInitials(this);
            }
        }

        //boidsParent = gameObject;
        switch (negibouringMethod)
        {
            case Negibouring.ThroughParent:
                foreach (Boid b in boids)
                {
                    
                }
                break;
            case Negibouring.ThroughCollision:
                break;
            default:
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}