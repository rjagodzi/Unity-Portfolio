using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{

    [SerializeField] Canvas canvasMessage;
    private bool inBuyZone = false;

    enum ItemType { healthRestore, healthUpgrade, weapon}
    [SerializeField] ItemType itemType;

    [SerializeField] int itemCost;

    [SerializeField] WeaponsSystem[] potentialWeapon;
    private WeaponsSystem weaponToBuy;

    [SerializeField] private SpriteRenderer ItemSpriteRenderer;
    [SerializeField] TMPro.TextMeshProUGUI priceText;

    private void Start()
    {
        if(itemType == ItemType.weapon)
        {
            int selectedWeapon = Random.Range(0, potentialWeapon.Length);
            weaponToBuy = potentialWeapon[selectedWeapon];

            itemCost = weaponToBuy.GetWeaponPrice();
            ItemSpriteRenderer.sprite = weaponToBuy.GetWeaponShopSprite();

            priceText.text = "Buy " + weaponToBuy.GetWeaponName() + ": " + itemCost + " coins";
        }
    }

    private void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(GameManager.instance.GetCurrentBitCoins() >= itemCost)
                {
                    GameManager.instance.SpendCoins(itemCost);

                    switch (itemType)
                    {
                        case ItemType.healthRestore:
                            FindObjectOfType<PlayerHealthHandler>().HealPlayer(25);
                            break;
                        case ItemType.healthUpgrade:
                            FindObjectOfType<PlayerHealthHandler>().IncreaseMaxHealth(25);
                            AudioManager.instance.PlaySFX(3);
                            break;
                        case ItemType.weapon:
                            PlayerController playerBuying = FindObjectOfType<PlayerController>();
                            WeaponsSystem weaponToAdd = Instantiate(weaponToBuy, playerBuying.GetWeaponsArm());
                            playerBuying.AddWeapon(weaponToAdd);
                            AudioManager.instance.PlaySFX(11);
                            break;
                        default:
                            Debug.Log("No item type was chosen");
                            break;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvasMessage.gameObject.SetActive(true);
        inBuyZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvasMessage.gameObject.SetActive(false);
        inBuyZone = false;
        
    }
}
