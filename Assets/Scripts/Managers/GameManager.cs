using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int currentCoins;

    private void Awake()
    {
        instance = this;      
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
