using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablesController : MonoBehaviour
{
    [SerializeField] GameObject[] brokenParts;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool playerIsDashing = collision.gameObject.GetComponent<PlayerController>().PlayerIsDashing();

            if (playerIsDashing)
            {
                GetComponent<Animator>().SetTrigger("Break");
                
                for(int i = 0; i < brokenParts.Length; i++)
                {
                    int randomBrokenPart = Random.Range(0, brokenParts.Length);
                    Instantiate(brokenParts[randomBrokenPart], transform.position, transform.rotation);
                }
                
            }
            
        }
    }

    public void Destroy()
    {
        //Instantiate(brokenParts, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
