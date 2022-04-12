using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float enemySpeed;
    [SerializeField] int enemyHealth = 100;
    private Rigidbody2D enemyRigidBody;

    [SerializeField] float playerChaseRange;
    [SerializeField] float keepChasingRange;
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
        if(Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange)
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

        if(directionToMoveIn != Vector3.zero)
        {
            enemyAnimator.SetBool("isWalking", true);
        }
        else
        {
            enemyAnimator.SetBool("isWalking", false);
        }

        if(playerToChase.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        if(!meleeAttack && readyToShoot)
        {
            readyToShoot = false;
            StartCoroutine(FireEnemyProjectile());
        }
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
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, keepChasingRange);

    }
}
