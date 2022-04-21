using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    [SerializeField] int healAmount = 25;
    private bool pickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickedUp && GetComponent<PickupDelayer>().canBePickedUpMethod())
        {
            pickedUp = true;
            collision.GetComponent<PlayerHealthHandler>().HealPlayer(healAmount);
            Destroy(gameObject);
        }
    }
}
