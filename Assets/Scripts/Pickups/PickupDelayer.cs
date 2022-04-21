using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDelayer : MonoBehaviour
{
    public static PickupDelayer instance;

    [SerializeField] float timeBeforePickup;
    private bool canPickUpItem;

    void Start()
    {
        canPickUpItem = false;
        StartCoroutine(DelayPickup());
    }

    public IEnumerator DelayPickup()
    {
        yield return new WaitForSeconds(timeBeforePickup);
        canPickUpItem = true;
    }

    public bool canBePickedUpMethod()
    {
        return canPickUpItem;
    }

}
