using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthHandler : MonoBehaviour
{

    [SerializeField] int bossMaxHealth = 500;
    public int bossCurrentHealth;

    private bool isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        bossCurrentHealth = bossMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageBoss(int damageTaken)
    {
        if (isInvincible)
        {
            return;
        }

        bossCurrentHealth -= damageTaken;

        if(bossCurrentHealth <= bossMaxHealth / 2)
        {
            GetComponent<Animator>().SetTrigger("GetAngry");
        }

        if(bossCurrentHealth <= 0)
        {
            GetComponent<Animator>().SetTrigger("Die");
        }
    }



    public int GetBossMaxHealth()
    {
        return bossMaxHealth;
    }

    public int GetBossCurrentHealth()
    {
        return bossCurrentHealth;
    }

    public void SetTheBossInvincible(bool isBossInvincible)
    {
        isInvincible = isBossInvincible;
    }

    public void DestroyTheBoss()
    {
        Destroy(gameObject);

    }

}
