using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles the logic for the tower buy menu at the bottom of the screen
/// </summary>
public class BuyTower : MonoBehaviour
{
    [SerializeField]
    private BuyManager buyManager;
    [SerializeField]
    private Tower towerToSpawn;
    [SerializeField]
    private Color normalBtnColor = Color.white;
    [SerializeField]
    private Color unAbleToBuyColor = Color.gray;
    [SerializeField]
    private Color ableToBuyColor = Color.white;
    [SerializeField]
    private TextMeshProUGUI buyPriceText;

    private bool canBuyTower = false;

    private void Start()
    {
        //Very ugly fix for a null pointer error
        //Put this here because the onEnable function gets called earlier then the awake of the Moneymanager
        //So if this isn't here then the instance of MoneyManager will be null
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.OnMoneyAmountChange += setBuyStatus;
    }

    private void OnEnable()
    {
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.OnMoneyAmountChange += setBuyStatus;

        buyPriceText.text = towerToSpawn.BuyCost.ToString();
    }

    private void OnDisable()
    {
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.OnMoneyAmountChange -= setBuyStatus;
    }

    private void setBuyStatus(int pMoney)
    {
        Image image = GetComponent<Image>();

        if (pMoney >= towerToSpawn.BuyCost)
        {
            canBuyTower = true;
            image.color = ableToBuyColor;
        }
        else
        {
            canBuyTower = false;
            image.color = unAbleToBuyColor;
        }
    }

    public void HandleTowerBuy()
    {
        if(canBuyTower)
            buyManager.HandleTowerBuy(towerToSpawn.gameObject);
    }
}
