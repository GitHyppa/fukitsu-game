using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    // Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    // Booleans
    bool shooting, readyToShoot, reloading;

    // Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public Canvas canvas;
    Text Text_AmmoCount;
    Text Text_Reloading;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        Text_AmmoCount = canvas.GetComponent<UIController>().Text_AmmoCount;
        Text_Reloading = canvas.GetComponent<UIController>().Text_Reloading;
        Text_Reloading.enabled = false;
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // Reload
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        // Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        } 
    }

    private void Shoot()
    {
        readyToShoot = false;

        // TODO: if player is moving, spread * 1.5
        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        // Raycast
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if(rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

        Text_AmmoCount.text = bulletsLeft + " / " + magazineSize;
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Text_Reloading.enabled = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        Text_Reloading.enabled = false;
        Text_AmmoCount.text = bulletsLeft + " / " + magazineSize;
    }
}
