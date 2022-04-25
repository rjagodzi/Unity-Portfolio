using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float enemySpeed;
    [SerializeField] int enemyHealth = 100;
    private Rigidbody2D enemyRigidBody;

    //enemies that chase player
    [SerializeField] bool shouldChasePlayer;
    [SerializeField] float playerChaseRange;
    [SerializeField] float playerKeepChasingRange;
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


    //enemies that run away
    [SerializeField] bool shouldRunAway;
    [SerializeField] float runawayRange;

    //

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

        float distancePlayerEnemy = Vector3.Distance(transform.position, playerToChase.position);

        if (distancePlayerEnemy < playerChaseRange && shouldChasePlayer)
        {
            isChasing = true;
            directionToMoveIn = playerToChase.position - transform.position;
        }
        else if (isChasing && distancePlayerEnemy < playerKeepChasingRange && shouldChasePlayer)
        {
            directionToMoveIn = playerToChase.position - transform.position;
        }
        else
        {
            isChasing = false;
            directionToMoveIn = Vector3.zero;
        }

        if(shouldRunAway && distancePlayerEnemy < runawayRange)
        {
            directionToMoveIn = transform.position - playerToChase.position;
        }

        directionToMoveIn.Normalize();
        enemyRigidBody.velocity = directionToMoveIn * enemySpeed;
    }

    //a Coroutine - suspends an instruction until certain conditions are met
    IEnumerator FireEnemyProjectile()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        if (playerToChase.gameObject.activeInHierarchy)
        {
            Instantiate(enemyProjectile, firePosition.position, firePosition.rotation);
            readyToShoot = true;

        }
    }

    public void DamageEnemy(int damageTaken)
    {
        enemyHealth -= damageTaken;

        //Instantiate(impactSplatter, transform.position, transform.rotation);

        if(enemyHealth <= 0)
        {
            Instantiate(deathSplatter, transform.position, transform.rotation);
            AudioManager.instance.PlaySFX(13);

            if (GetComponent<ItemDropper>() != null)
            {
                if (GetComponent<ItemDropper>().IsItemDropper())
                {
                    GetComponent<ItemDropper>().DropItem();
                }
            }

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootPlayerRange);

        if (shouldChasePlayer)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, playerChaseRange);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, playerKeepChasingRange);
        }
        

        if (shouldRunAway)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, runawayRange);
        }

    }
}
