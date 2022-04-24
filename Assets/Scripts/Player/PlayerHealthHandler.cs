using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    [SerializeField] float invincibilityTime = 1f;
    private bool isInvincible;

    [SerializeField] SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIManager.instance.healthSlider.maxValue = maxHealth;

        PlayerHealthUpdateUI();

        isInvincible = false;
    }

    public void DamagePlayer(int damageTaken)
    {
        if (!isInvincible)
        {
            currentHealth -= damageTaken;
            AudioManager.instance.PlaySFX(6);
            PlayerHealthUpdateUI();

            MakePlayerInvincible();

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                UIManager.instance.DisplayDeathScreen();
            }
        }
    }

    public void MakePlayerInvincible()
    {
        isInvincible = true;

        StartCoroutine(Flasher());
        StartCoroutine(PlayerNotInvincible());
    }

    IEnumerator Flasher()
    {
        for(int i=0; i < 5; i++)
        {
            playerSprite.color = new Color(
                playerSprite.color.r,
                playerSprite.color.g,
                playerSprite.color.b,
                0.1f
                );

            yield return new WaitForSeconds(.1f);

            playerSprite.color = new Color(
                playerSprite.color.r,
                playerSprite.color.g,
                playerSprite.color.b,
                1f
                );

            yield return new WaitForSeconds(.1f);
        }
    }
    
    IEnumerator PlayerNotInvincible()
    {
        yield return new WaitForSeconds(invincibilityTime);

        isInvincible = false;
    }

    public void PlayerHealthUpdateUI()
    {
        UIManager.instance.healthSlider.value = currentHealth;
        UIManager.instance.healthText.text = currentHealth + "/" + maxHealth;
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        AudioManager.instance.PlaySFX(3);

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            AudioManager.instance.PlaySFX(3);
        }

        PlayerHealthUpdateUI();

    }

}
