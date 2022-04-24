using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponsChest : MonoBehaviour
{

    [SerializeField] WeaponsPickups[] potentialWeapon;
    private SpriteRenderer chestSR;

    [SerializeField] Sprite openChestSprite;
    [SerializeField] TextMeshProUGUI openKeyText;

    [SerializeField] Transform spawnPoint;

    private bool canOpen, hasBeenOpened;

    // Start is called before the first frame update
    void Start()
    {
        chestSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        OpenTheChest();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        openKeyText.gameObject.SetActive(true);
        canOpen = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!hasBeenOpened)
        {
            openKeyText.gameObject.SetActive(false);
        }

        canOpen = false;
    }
     
    private void OpenTheChest()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen && !hasBeenOpened)
        {
            chestSR.sprite = openChestSprite;
            AudioManager.instance.PlaySFX(12);
            int randomWeapon = Random.Range(0, potentialWeapon.Length);
            Instantiate(potentialWeapon[randomWeapon], spawnPoint.transform.position, spawnPoint.transform.rotation);
            hasBeenOpened = true;
        }
    }

}
