using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenParts : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    private Vector3 movementDirection;

    [SerializeField] float haltingFacor = 5f;

    // Start is called before the first frame update
    void Start()
    {
        movementDirection.x = Random.Range(-moveSpeed, moveSpeed);
        movementDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movementDirection * Time.deltaTime;

        movementDirection = Vector3.Lerp(movementDirection, Vector3.zero, haltingFacor * Time.deltaTime);
    }
}
