using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side : MonoBehaviour
{
    [SerializeField] Side connectedTo;
    [SerializeField] Transform arrivalPlace;

    LevelManager levelManager;

    private void Start()
    {
        levelManager = GetComponentInParent<LevelManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag==Constants.PlayerTag_KEY)
        {
            levelManager.SendPlayerTo(connectedTo.arrivalPlace);
        }
    }
}