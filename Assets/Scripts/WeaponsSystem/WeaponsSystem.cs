using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSystem : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;

    private float shotCounter = 0;
    [SerializeField] float timeBetweenShots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FiringBullets();
    }

    private void FiringBullets()
    {
        if (GetComponentInParent<PlayerController>().PlayerIsDashing()) {return;}

        if(shotCounter > 0)
        {
            shotCounter -= Time.deltaTime;
        }
        else
        {
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }

        //if (Input.GetMouseButtonDown(0) && !isWeaponAutomatic)
        //{
        //    // Instantiate the projectile at the position and rotation of this transform
        //    Instantiate(bullet, firePoint.position, firePoint.rotation);
        //}

        //if (Input.GetMouseButton(0) && isWeaponAutomatic)
        //{
        //    shotCounter -= Time.deltaTime;

        //    if (shotCounter <= 0)
        //    {
        //        Instantiate(bullet, firePoint.position, firePoint.rotation);
        //        shotCounter = timeBetweenShots;
        //    }
        //}
    }

}
