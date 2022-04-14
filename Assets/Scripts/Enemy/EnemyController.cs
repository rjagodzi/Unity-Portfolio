using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float enemySpeed;
    [SerializeField] int enemyHealth = 100;
    private Rigidbody2D enemyRigidBody;

    [SerializeField] float chasePlayerRange;
    [SerializeField] float keepChasingRange;
    [SerializeField] float shootPlayerRange;
    private Vector3 directionToMoveIn;
    private bool isChasing;

    private Transform playerToChase;

    private Animator enemyAnimator;

    [SerializeField] bool meleeAttack;

    [SerializeField] GameObject enemyProjectile;
    [SerializeField] Transform firePosition;

    [SerializeField] float timeBetweenShots;
    private bool readyToShoot;

    [SerializeField] GameObject deathSplatter;
    //[SerializeField] GameObject impactSplatter;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        playerToChase = FindObjectOfType<PlayerController>().transform;

        enemyAnimator = GetComponentInChildren<Animator>();
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyChasingPlayer();
        EnemyWalkAnimation();
        EnemyTurningTowardsPlayer();
        EnemyShootingPlayerWhenInRange();
    }

    private void EnemyShootingPlayerWhenInRange()
    {
        if (!meleeAttack && readyToShoot && Vector3.Distance(playerToChase.transform.position, transform.position) < shootPlayerRange)
        {
            readyToShoot = false;
            StartCoroutine(FireEnemyProjectile());
        }
    }

    private void EnemyTurningTowardsPlayer()
    {
        if (playerToChase.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    private void EnemyWalkAnimation()
    {
        if (directionToMoveIn != Vector3.zero)
        {
            enemyAnimator.SetBool("isWalking", true);
        }
        else
        {
            enemyAnimator.SetBool("isWalking", false);
        }
    }

    private void EnemyChasingPlayer()
    {
        if (Vector3.Distance(transform.position, playerToChase.position) < chasePlayerRange)
        {
            isChasing = true;
            directionToMoveIn = playerToChase.position - transform.position;
        }
        else if (isChasing && Vector3.Distance(transform.position, playerToChase.position) < keepChasingRange)
        {
            directionToMoveIn = playerToChase.position - transform.position;
        }
        else
        {
            isChasing = false;
            directionToMoveIn = Vector3.zero;
        }

        directionToMoveIn.Normalize();
        enemyRigidBody.velocity = directionToMoveIn * enemySpeed;
    }

    //a Coroutine - suspends an instruction until certain conditions are met
    IEnumerator FireEnemyProjectile()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        Instantiate(enemyProjectile, firePosition.position, firePosition.rotation);
        readyToShoot = true;
    }

    public void DamageEnemy(int damageTaken)
    {
        enemyHealth -= damageTaken;

        //Instantiate(impactSplatter, transform.position, transform.rotation);

        if(enemyHealth <= 0)
        {
            Instantiate(deathSplatter, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chasePlayerRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, keepChasingRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootPlayerRange);

    }
}
