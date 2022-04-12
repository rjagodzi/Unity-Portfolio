using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] GameObject bulletImpactEffect;
    [SerializeField] GameObject[] damageEffects;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] int damageAmmount = 10;

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
            int randomSplash = Random.Range(0, damageEffects.Length);

            Instantiate(damageEffects[randomSplash], transform.position, transform.rotation);
            collision.GetComponent<EnemyController>().DamageEnemy(damageAmmount);
        }
        else
        {
            Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        }
        
        Destroy(gameObject);
    }

}
