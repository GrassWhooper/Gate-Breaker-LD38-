using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 3;
    public GameObject hitEffect = null;
    public float extraDetectionRange = 0.02f;
    public float sphereCastRadious = 0.5f;
    float damage;
    LayerMask layerToDetect;
    Vector3 lastPos = Vector3.zero;
    public void TakeInitials(float damage,LayerMask whatItCanHit)
    {
        this.damage = damage;
        layerToDetect = whatItCanHit;
    }
    private void Start()
    {
        lastPos = transform.position;
    }
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        Vector3 direction = (-transform.position+ lastPos).normalized;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit whatWasHit;
        bool didHit = Physics.SphereCast(ray, sphereCastRadious, out whatWasHit, direction.magnitude + extraDetectionRange, layerToDetect);
        Debug.DrawRay(transform.position, direction * (direction.magnitude + extraDetectionRange), Color.red);
        if (didHit==true)
        {
            if (hitEffect!=null)
            {
                Instantiate(hitEffect, whatWasHit.point, Quaternion.identity);
            }
            
            Health h = whatWasHit.collider.GetComponent<Health>();
            
            h.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}