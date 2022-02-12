using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public float currentAmmo = 30f;
    float timeLeft = 2f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject bloodEffect;
    public Text ammoCount;
    public Text reloading;

    private float NextTimeToFire = 0f;

    void Start()
    {
        ammoCount.text = "30 / 30";
        reloading.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if(Input.GetKeyDown("r"))
        {
            reloading.enabled = true;
            timeLeft = 2f;
            Debug.Log("123");
        }

        if(reloading.enabled)
        {
            if (timeLeft < 0)
            {
                reloading.enabled = false;
                currentAmmo = 30f;
                ammoCount.text = currentAmmo + " / 30";
            } 
            else
            {
                timeLeft -= Time.deltaTime;

            }
        }
    }

    void Shoot()
    {
        if(currentAmmo > 0)
        {
            muzzleFlash.Play();
            RaycastHit hit;

            currentAmmo = currentAmmo - 1f;
            ammoCount.text = currentAmmo + " / 30";

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                GameObject impactBloodGO = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
                Destroy(impactBloodGO, 0.25f);
            }
        }
    }
}
