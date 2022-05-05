using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] GameObject bulletImpactEffect;
    [SerializeField] GameObject[] damageEffects;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] int damageAmount = 10;

    private Rigidbody2D bulletRigidbody;
    

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.right enables the bullet to travel along its red axis
        bulletRigidbody.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            InstantiateBloodSplatter();
            collision.GetComponent<EnemyController>().DamageEnemy(damageAmount);
        }
        else if (collision.gameObject.CompareTag("BreakableObject"))
        {
            Instantiate(bulletImpactEffect, transform.position, transform.rotation);
            collision.GetComponent<Animator>().SetTrigger("Break");
            AudioManager.instance.PlaySFX(0);
            
            if (collision.GetComponent<ItemDropper>() != null)
            {
                if (collision.GetComponent<ItemDropper>().IsItemDropper())
                {
                    collision.GetComponent<ItemDropper>().DropItem();
                }
            }
        }else if (collision.CompareTag("Boss"))
        {
            InstantiateBloodSplatter();
            collision.GetComponent<BossHealthHandler>().DamageBoss(damageAmount);
        }
        else
        {
            Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        }
        
        Destroy(gameObject);
    }

    private void InstantiateBloodSplatter()
    {
        int randomSplash = Random.Range(0, damageEffects.Length);
        AudioManager.instance.PlaySFX(1);

        Instantiate(damageEffects[randomSplash], transform.position, transform.rotation);
    }
}
