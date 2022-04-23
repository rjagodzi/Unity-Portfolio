using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsChest : MonoBehaviour
{

    [SerializeField] WeaponsPickups[] potentialWeapon;
    private SpriteRenderer chestSR;

    [SerializeField] Sprite openChestSprite;
    [SerializeField] Text openKeyText;

    private bool canOpen;

    // Start is called before the first frame update
    void Start()
    {
        chestSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
