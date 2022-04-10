using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float enemySpeed;
    private Rigidbody2D enemyRigidBody;

    [SerializeField] float playerChaseRange;
    private Vector3 directionToMoveIn;

    private Transform playerToChase;

    // Start is called before the first frame update
    void Start()
    {
        playerToChase = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange)
        {
            Debug.Log("Player in chase range");
        }
        else
        {
            Debug.Log("Player out of chase range");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);
    }
}
