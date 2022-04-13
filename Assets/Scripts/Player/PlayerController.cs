using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRigidbody;

    [SerializeField] Transform weaponsArm;

    private Camera mainCamera;

    [SerializeField] int movementSpeed;

    private Vector2 movementInput;

    private Animator playerAnimator;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;

    [SerializeField] bool isWeaponAutomatic;
    [SerializeField] float timeBetweenShots;
    private float shotCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoving();
        PointingGunAtMouse();
        AnimatingPlayer();
        PlayerShooting();

    }

    private void AnimatingPlayer()
    {
        if (movementInput != Vector2.zero)
        {
            playerAnimator.SetBool("isMoving", true);
        }
        else
        {
            playerAnimator.SetBool("isMoving", false);

        }
    }

    private void PointingGunAtMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        weaponsArm.rotation = Quaternion.Euler(0, 0, angle);

        //if statement for turning the player and weaponArm sprites left or right depending on the mouse position

        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponsArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            weaponsArm.localScale = Vector3.one;
        }
    }

    private void PlayerShooting()
    {
        if (Input.GetMouseButtonDown(0) && !isWeaponAutomatic)
        {
            // Instantiate the projectile at the position and rotation of this transform
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }

        if (Input.GetMouseButton(0) && isWeaponAutomatic)
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
    }

    private void PlayerMoving()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        //normalizing movement input ensures that the magnitude of diagonal movement is the same as vertical and horizontal movements

        movementInput.Normalize();

        //the method below is not sufficient - it causes jittering when coliding with walls
        //transform.position += new Vector3(movementInput.x, movementInput.y, 0f) * movementSpeed * Time.deltaTime;

        playerRigidbody.velocity = movementInput * movementSpeed;
    }
}
