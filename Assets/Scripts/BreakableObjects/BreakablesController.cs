using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablesController : MonoBehaviour
{
    [SerializeField] GameObject[] brokenParts;
    public static BreakablesController instance;

    private void Start()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool playerIsDashing = collision.gameObject.GetComponent<PlayerController>().PlayerIsDashing();

            if (playerIsDashing)
            {

                GetComponent<Animator>().SetTrigger("Break");

                for (int i = 0; i < brokenParts.Length; i++)
                {
                    int randomBrokenPart = Random.Range(0, brokenParts.Length);
                    int randomRotation = Random.Range(0, 4);

                    Instantiate(
                        brokenParts[randomBrokenPart],
                        transform.position,
                        Quaternion.Euler(0f, 0f, 90f * randomRotation));
                }

                if (GetComponent<ItemDropper>() != null)
                {
                    if (GetComponent<ItemDropper>().IsItemDropper())
                    {
                        GetComponent<ItemDropper>().DropItem();
                    }
                }

            }

        }

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
