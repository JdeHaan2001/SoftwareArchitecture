using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    int money = 100;

    public Action<int> OnMoneyAmountChange;
    public static MoneyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        OnMoneyAmountChange?.Invoke(money);
    }

    public void AddMoney(int pMoney)
    {
        money += pMoney;
        OnMoneyAmountChange?.Invoke(money);
    }

    public void RemoveMoney(int pMoney)
    {
        money -= pMoney;
        OnMoneyAmountChange?.Invoke(money);
    }
}
