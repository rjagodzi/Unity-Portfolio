using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DamagePlayer(int damageTaken)
    {
        currentHealth -= damageTaken;

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
