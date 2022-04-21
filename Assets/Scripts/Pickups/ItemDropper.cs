using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{

    [SerializeField] bool dropsItems;
    [SerializeField] GameObject[] itemsToDrop;
    [SerializeField] float itemDropChance = 0.5f;

    public void DropItem()
    {
        if (Random.value < itemDropChance)
        {
                int randomItemNumber = Random.Range(0, itemsToDrop.Length);

                Instantiate(itemsToDrop[randomItemNumber], transform.position, transform.rotation);
        }
    }

    public bool IsItemDropper()
    {
        return dropsItems;
    }
}
