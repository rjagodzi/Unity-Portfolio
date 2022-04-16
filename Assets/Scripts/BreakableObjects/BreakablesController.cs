using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablesController : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") 
            && player.GetComponent<PlayerController>().PlayerIsDashing())
        {
            GetComponent<Animator>().SetTrigger("Break");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
