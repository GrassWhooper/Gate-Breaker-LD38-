using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants 
{
    public const string horiAxis_KEY = "Horizontal";
    public const string vertAxis_KEY = "Vertical";
}

public class MovementVariables
{
    public enum TiltAxis { X, Y, Both }
    public TiltAxis tiltAxis = TiltAxis.Both;
    public float readForwardSpeed = 30;
    public float movementSpeed = 7.5f;
    public float rotationSpeed = 180.0f;
    public float tiltSpeed = 60.0f;
    public float tiltDegree = 15.0f;
    public float smoothTime = 0.1f;

    public MovementVariables(float readForward,float smoothTime, float movementSpeed, float rotationSpeed, float tiltSpeed, float tiltDegree,TiltAxis tiltAxis)
    {
        readForwardSpeed = readForward;
        this.movementSpeed = movementSpeed;
        this.rotationSpeed = rotationSpeed;
        this.tiltSpeed = tiltSpeed;
        this.tiltDegree = tiltDegree;
        this.smoothTime = smoothTime;
        this.tiltAxis = tiltAxis;
    }
}