using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovementController : MonoBehaviour {

    Vector3 newPos = new Vector3();
    Vector3 lastPos = new Vector3();
    Vector3 movingTowards = new Vector3();
    Vector3 refVel = new Vector3();
    float expectationStep { get { return movementVariables.readForwardSpeed * Time.deltaTime; } }
    float movementStep { get { return movementVariables.movementSpeed * Time.deltaTime; } }
    float rotationStep { get { return movementVariables.rotationSpeed * Time.deltaTime; } }
    float tiltStep { get { return movementVariables.tiltSpeed * Time.deltaTime; } }
    float smoothTime { get { return movementVariables.smoothTime; } }
    float tiltDegree { get { return movementVariables.tiltDegree; } }
    Quaternion targetRotation = new Quaternion();
    Quaternion targetTiltRot = new Quaternion();
    MovementVariables movementVariables;

    public void TakeMovementVariable(MovementVariables movementVariables)
    {
        this.movementVariables = movementVariables;

    }

    private void Start()
    {
        lastPos = transform.position;
        newPos = lastPos;
    }

    public void Move(Vector3 input)
    {
        Movement(input);
        LookForward(input);
        Tilt(input);
    }

    void Tilt(Vector3 input)
    {
        float tiltDirectionX = 0;
        float tiltDirectionY = 0;
        
        switch (movementVariables.tiltAxis)
        {
            case MovementVariables.TiltAxis.X:
                tiltDirectionX = Mathf.Sign(input.y);
                tiltDirectionY = 0;
                break;
            case MovementVariables.TiltAxis.Y:
                tiltDirectionY = Mathf.Sign(input.x);
                tiltDirectionX = 0;
                break;
            case MovementVariables.TiltAxis.Both:
                tiltDirectionX = Mathf.Sign(input.y);
                tiltDirectionY = Mathf.Sign(input.x);
                break;
            default:
                break;
        }
        if (input.y == 0)
        {
            tiltDirectionX = 0;
        }
        if (input.x==0)
        {
            tiltDirectionY = 0;
        }
        Vector3 targetEU = new Vector3(tiltDirectionX * tiltDegree, tiltDirectionY * tiltDegree, transform.rotation.eulerAngles.z);
        targetTiltRot = Quaternion.Euler(targetEU);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetTiltRot, tiltStep);
    }
    void Movement(Vector3 input)
    {
        newPos = lastPos + input * expectationStep;
        movingTowards = Vector3.SmoothDamp(movingTowards, newPos, ref refVel, smoothTime);
        transform.position = Vector3.MoveTowards(transform.position, movingTowards, movementStep);
        lastPos = transform.position;
    }
    void LookForward(Vector3 input)
    {

        if (input.magnitude != 0)
        {
            if ((newPos - lastPos).magnitude != 0)
            {
                targetRotation = Quaternion.LookRotation(transform.forward, newPos - lastPos);
            }
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, rotationStep);
        }
    }


}
