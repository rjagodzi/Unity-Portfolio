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

        UIManager.instance.healthSlider.maxValue = maxHealth;

        PlayerHealthUpdate();
    }

    private void Update()
    {
        PlayerHealthUpdate();
    }

    public void DamagePlayer(int damageTaken)
    {
        currentHealth -= damageTaken;
        PlayerHealthUpdate();

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

    }

    public void PlayerHealthUpdate()
    {
        UIManager.instance.healthSlider.value = currentHealth;
        UIManager.instance.healthText.text = currentHealth + "/" + maxHealth;
    }

}
