using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSystem : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;

    private float shotCounter = 0;
    [SerializeField] float timeBetweenShots;

    [SerializeField] Sprite weaponImage;
    [SerializeField] string weaponName;

    //creating a method that would take the weaponSFX variable
    //and use it to play a specific SFX track based on the weapon fired
    //was one of my proudest moments during the creation of this game
    //and it made me realize how much I've learned
    [SerializeField] int weaponSFX;

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
                PlayWeaponSFX();
                shotCounter = timeBetweenShots;
            }
        }
    }

    public Sprite GetWeaponImageUI()
    {
        return weaponImage;
    }

    public string GetWeaponName()
    {
        return weaponName;
    }

    public void PlayWeaponSFX()
    {
        AudioManager.instance.PlaySFX(weaponSFX);
    }

}
