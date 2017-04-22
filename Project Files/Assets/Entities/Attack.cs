using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Essentials")]
    public Transform shootSpot = null;
    public Projectile projectile = null;
    public LayerMask layerToHit;
    [Header("Attack Propreties")]
    public float vanishAfter = 3.0f;
    public float damage = 1.0f;
    public float timeBetweenShots = 0.2f;
    [Header("Asethtics")]
    public GameObject attackEffect;

    Coroutine attackRotine = null;
    float timeBetweenShotsCounter = 0;
    private void Update()
    {
        timeBetweenShotsCounter -= Time.deltaTime;
    }
    public void StartAttack()
    {
        if (attackRotine==null && timeBetweenShotsCounter<=0)
        {
            timeBetweenShotsCounter = timeBetweenShots;
            attackRotine = StartCoroutine(BeginShooting());
        }
    }
    public void StopAttack()
    {
        if (attackRotine!=null)
        {
            StopCoroutine(attackRotine);
            attackRotine = null;
        }
    }
    IEnumerator BeginShooting()
    {
        yield return 0;
        while (true)
        {
            DoShoot();
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
    public void DoShoot()
    {
        GameObject spawnedObject = Instantiate(projectile.gameObject, shootSpot.transform.position, shootSpot.transform.rotation);
        Projectile spawnedProj = spawnedObject.GetComponent<Projectile>();
        spawnedProj.TakeInitials(damage, layerToHit);
        if (vanishAfter != 0)
        {
            Destroy(spawnedObject, vanishAfter);
        }
        if (attackEffect!=null)
        {
            Instantiate(attackEffect, shootSpot.transform.position, shootSpot.transform.rotation);
        }
    }
}