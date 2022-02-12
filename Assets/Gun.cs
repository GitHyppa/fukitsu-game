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
    public float maxAmmo = 30f;
    public float currentAmmo;
    float timeLeft = 2f;

    public Canvas canvas;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject bloodEffect;
    Text Text_AmmoCount;
    Text Text_Reloading;

    private float NextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
        Text_AmmoCount = canvas.GetComponent<UIController>().Text_AmmoCount;
        Text_Reloading = canvas.GetComponent<UIController>().Text_Reloading;

        Text_AmmoCount.text = "30 / 30";
        Text_Reloading.enabled = false;
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
            if(currentAmmo < maxAmmo && Text_Reloading.enabled == false)
            {
                Text_Reloading.enabled = true;
                timeLeft = 2f;
            }
        }

        if(Text_Reloading.enabled)
        {
            if (timeLeft < 0)
            {
                Text_Reloading.enabled = false;  
                currentAmmo = 30f;
                Text_AmmoCount.text = currentAmmo + " / 30";
            } 
            else
            {
                timeLeft -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0 && Text_Reloading.enabled == false)
        {
            muzzleFlash.Play();
            RaycastHit hit;

            currentAmmo = currentAmmo - 1f;
            Text_AmmoCount.text = currentAmmo + " / 30";

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
