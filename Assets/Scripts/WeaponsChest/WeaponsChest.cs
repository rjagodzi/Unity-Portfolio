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

    private bool canOpen;

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
        if (collision.CompareTag("Player"))
        {
            openKeyText.gameObject.SetActive(true);
            canOpen = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        openKeyText.gameObject.SetActive(false);
        canOpen = false;
    }

    private void OpenTheChest()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen)
        {
            chestSR.sprite = openChestSprite;
        }
    }

}
