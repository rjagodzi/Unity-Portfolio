using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] GameObject bulletImpactEffect;
    [SerializeField] float bulletSpeed = 5f;

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
        Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
