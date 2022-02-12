using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    public float range = 100f;
    public float fireRate = 15f;
    public float damage = 100f;
    private float NextTimeToFire = 0f;

    public GameObject FPSCamera;
    public float effectSpeed = 50;
    public GameObject MagicTrailEffect;
    public GameObject MagicImpactEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2") && Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        //Make magic ray
        GameObject magicTrail = Instantiate(MagicTrailEffect, FPSCamera.transform.position, FPSCamera.transform.rotation);
        Rigidbody rb = magicTrail.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = FPSCamera.transform.forward * effectSpeed;

        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactEffect = Instantiate(MagicImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactEffect, 0.5f);
            Destroy(magicTrail, Vector3.Distance(hit.point, FPSCamera.transform.position) / effectSpeed);
        }
        else
        {
            Destroy(magicTrail, range/effectSpeed);
        }


        
    }
}
