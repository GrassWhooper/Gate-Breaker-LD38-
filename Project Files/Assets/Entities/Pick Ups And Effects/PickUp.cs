using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] float dieAFter = 5.0f;
    [SerializeField] GameObject healingEffect = null;
    [SerializeField] float healthAmount = 15;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag==Constants.PlayerTag_KEY)
        {
            Transform t = other.transform.root;
            Health h = t.GetComponentInChildren<Health>();
            float hpIncrease = Mathf.Abs(healthAmount);
            hpIncrease = hpIncrease * (-1);
            h.TakeDamage(hpIncrease);
            Vector3 spawnPos = transform.position;
            spawnPos.z = 0;
            if (healingEffect != null)
            {
                Instantiate(healingEffect, spawnPos, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag==Constants.PlayerTag_KEY)
    //    {
    //        Transform t=  other.transform.root;
    //        Health h = t.GetComponentInChildren<Health>();
    //        h.TakeDamage(Mathf.Abs(healthAmount) * -1);
    //    }
    //    if (healingEffect!=null)
    //    {
    //        Instantiate(healingEffect, transform.position, Quaternion.identity);
    //    }
    //    Destroy(gameObject);
    //}
}