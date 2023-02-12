using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField][Range(0, 500)]
    private int money = 25;

    public Action<int> OnMoneyAmountChange;
    public static MoneyManager Instance;

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

    private void Update()
    {
        //For testing purposes only
        if (Input.GetKeyDown(KeyCode.M))
            AddMoney(5);
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

    public int GetMoneyAmount() => money;
}
