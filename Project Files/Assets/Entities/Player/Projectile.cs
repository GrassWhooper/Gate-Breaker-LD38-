using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 3;

    public float extraDetectionRange = 0.02f;
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
        bool didHit = Physics.Raycast(ray, out whatWasHit, direction.magnitude + extraDetectionRange, layerToDetect);
        Debug.DrawRay(transform.position, direction * (direction.magnitude + extraDetectionRange), Color.red);
        if (didHit==true)
        {
            Debug.Log(whatWasHit.collider.name);
        }
    }
}