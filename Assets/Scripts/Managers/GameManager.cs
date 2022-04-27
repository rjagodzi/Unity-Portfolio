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

    private void Start()
    {
        UIManager.instance.UpdateCoinText(currentCoins);
    }

    public void AddCoins(int amountToAdd)
    {
        currentCoins += amountToAdd;
        UIManager.instance.UpdateCoinText(currentCoins);
    }

    public void SpendCoins(int amountToRemove)
    {
        currentCoins -= amountToRemove;

        if (currentCoins <= 0)
        {
            currentCoins = 0;
        }

        UIManager.instance.UpdateCoinText(currentCoins);

    }

    public int GetCurrentBitCoins()
    {
        return currentCoins;
    }

}
