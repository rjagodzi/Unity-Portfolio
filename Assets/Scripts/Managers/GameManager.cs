using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currentCoins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddCoins(20);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            RemoveCoins(10);
        }

        Debug.Log("I have: " + currentCoins + " coins");
    }

    public void AddCoins(int amountToAdd)
    {
        currentCoins += amountToAdd;
    }

    public void RemoveCoins(int amountToRemove)
    {
        currentCoins -= amountToRemove;

        if (currentCoins <= 0)
        {
            currentCoins = 0;
        }
    }
}
