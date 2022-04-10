using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float enemySpeed;
    private Rigidbody2D enemyRigidBody;

    [SerializeField] float playerChaseRange;
    [SerializeField] float keepChasingRange;
    private Vector3 directionToMoveIn;
    private bool isChasing;

    private Transform playerToChase;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        playerToChase = FindObjectOfType<PlayerController>().transform;
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, keepChasingRange);

    }
}
