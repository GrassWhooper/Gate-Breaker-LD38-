using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(Attack))]
public class PlayerInputManager : MonoBehaviour
{
    public float readForwardSpeed = 30;
    public float smoothTime = 0.1f;
    public float movementSpeed = 7.5f;
    public float rotationSpeed = 180.0f;
    public float tiltSpeed = 60.0f;
    public float tiltDegree = 15.0f;
    public MovementVariables.TiltAxis tiltAxis = MovementVariables.TiltAxis.Both;

    MovementController movementController = null;
    Attack attack = null;
    float timeBetweenShotsCounter = 0;

    void Start()
    {
        movementController = GetComponent<MovementController>();
        movementController.TakeMovementVariable(new MovementVariables(
            readForwardSpeed, smoothTime, movementSpeed, rotationSpeed, tiltSpeed, tiltDegree, tiltAxis));
        attack = GetComponent<Attack>();
    }
    void Update()
    {
        float x = Input.GetAxis(Constants.horiAxis_KEY);
        float y = Input.GetAxis(Constants.vertAxis_KEY);
        Vector3 input = (new Vector3(x, y));
        if (input.magnitude >= 1)
        {
            input.Normalize();
        }
        movementController.Move(input);



        if (Input.GetButtonDown(Constants.Fire_KEY)
            || Input.GetKeyDown(KeyCode.LeftControl)
            || Input.GetKeyDown(KeyCode.RightControl))
        {
            attack.StartAttack();
        }
        else if (Input.GetButtonUp(Constants.Fire_KEY)
            || Input.GetKeyUp(KeyCode.LeftControl)
            || Input.GetKeyUp(KeyCode.RightControl))
        {
            attack.StopAttack();
        }
    }
}