using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Transform playerToChase;
    private bool isFlipped = false;

    [SerializeField] int damageAmount = 35;
    [SerializeField] int angryDamageAmount = 50;
    [SerializeField] Transform pointOfAttack;

    [SerializeField] float attackRadius;
    [SerializeField] float angryAttackRadius;
    [SerializeField] LayerMask whatIsPlayer;

    [SerializeField] Transform[] shootingPoints;
    [SerializeField] Transform[] angryShootingPoints;

    [SerializeField] GameObject bossBullet;

    // Start is called before the first frame update
    void Start()
    {
        playerToChase = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        BossTurningTowardsPlayer();
    }

    private void BossTurningTowardsPlayer()
    {
        if (playerToChase.position.x < transform.position.x && isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if(playerToChase.position.x > transform.position.x && !isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void AttackPlayer()
    {
        Collider2D playerToAttack = Physics2D.OverlapCircle(pointOfAttack.position, attackRadius, whatIsPlayer);

        if(playerToAttack != null)
        {
            playerToAttack.GetComponent<PlayerHealthHandler>().DamagePlayer(damageAmount);
        }
    }

    public void AngryAttackPlayer()
    {
        Collider2D playerToAttack = Physics2D.OverlapCircle(pointOfAttack.position, angryAttackRadius, whatIsPlayer);

        if (playerToAttack != null)
        {
            playerToAttack.GetComponent<PlayerHealthHandler>().DamagePlayer(angryDamageAmount);
        }
    }

    public void ShootingPlayer()
    {
        foreach(Transform point in shootingPoints)
        {
            Instantiate(bossBullet, point.position, point.rotation);
        }
    }

    public void AngryShootingPlayer()
    {
        foreach (Transform point in angryShootingPoints)
        {
            Instantiate(bossBullet, point.position, point.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointOfAttack.position, attackRadius);
    }

}
