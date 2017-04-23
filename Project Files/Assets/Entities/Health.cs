using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] GameObject deathEffect = null;
    [SerializeField] float health = 6;
    [Header("Pick Up Details")]
    [SerializeField] GameObject pickUp;
    [SerializeField] float spawnChance = 0.3f;
    
    public float CurrentHealth { get { return health; } }
    public delegate void OnDeathDelegate();
    public event OnDeathDelegate OnDeathEvent;
    public delegate void OnDamageTaken();
    public event OnDamageTaken onDamageTakenEvent;
    public void TakeDamage(float damage)
    {
        if (onDamageTakenEvent!=null)
        {
            onDamageTakenEvent();
        }
        health -= damage;
        if (health<0)
        {
            if (deathEffect)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
            if (OnDeathEvent!=null)
            {
                OnDeathEvent();
            }
            float r = Random.value;
            if (spawnChance>=r && pickUp != null)
            {
                Instantiate(pickUp, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}