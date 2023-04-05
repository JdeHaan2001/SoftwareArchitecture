using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles everything that is money related. Public function can get accessed through a singleton Instance.
/// </summary>
public class MoneyManager : MonoBehaviour
{
    [SerializeField][Range(0, 500)]
    private int startMoney = 100;

    private int money;

    public event Action<int> OnMoneyAmountChange;
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
        money = startMoney;
        OnMoneyAmountChange?.Invoke(money);
    }

    private void Update()
    {
        //For testing purposes only
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M))
            AddMoney(5);
#endif
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
