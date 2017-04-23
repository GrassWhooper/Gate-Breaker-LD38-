using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gate : MonoBehaviour
{
    public delegate void EnteredGateDelegate();
    public event EnteredGateDelegate EnteredGateEvent;
    public delegate void LeftGateDelegate();
    public event LeftGateDelegate LeftGateEvent;
    public bool GotAnswer{get { return gotAnswer; }set{gotAnswer = value;}}

    bool gotAnswer = false;
    Transform player;
    Quaternion targetRotation;

    void Start()
    {
        player = FindObjectOfType<PlayerInputManager>().transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.PlayerTag_KEY)
        {
            if (EnteredGateEvent != null)
            {
                EnteredGateEvent();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Constants.PlayerTag_KEY)
        {
            if (LeftGateEvent!=null)
            {
                LeftGateEvent();
            }
        }
    }
}