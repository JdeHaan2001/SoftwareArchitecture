using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This is the base class for the towers. A new tower type can inherit from this class to get some base logic like buying, upgrading or selling the tower.
/// </summary>
public class Tower : MonoBehaviour
{
    public int Damage = 5;
    public int BuyCost = 10;
    public int UpgradeCost = 15;
    public int SellProfit = 7;
    public float Range = 50f;

    //The properties below are protected, because they should only be accessed by classes inheriting from this class.
    //They have the SerializeField component, because I want them to be changed in the inspector so that e.g. a designer can quickly change some values
    [SerializeField]
    protected float UpgradeMultiplier = 1.3f;
    [SerializeField]
    protected float RateOfFire = 1.5f;
    [SerializeField]
    protected Canvas upgradeBtnCanvas = null;
    [SerializeField]
    protected TextMeshProUGUI upgradeCostText;
    [SerializeField]
    protected TextMeshProUGUI sellProfitText;

    protected Enemy target;
    protected bool isShooting = false;
    protected Button upgradeButton;
    protected Button sellButton;

    private void Start()
    {
        //Very ugly fix for a null pointer error
        //Put this here because the onEnable function gets called earlier then the awake of the Moneymanager
        //So if this isn't here then the instance of MoneyManager will be null
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.OnMoneyAmountChange += setUpgradeButtonStatus;

        upgradeCostText.text = $"<b>Upgrade</b>\n{UpgradeCost}g";
        sellProfitText.text = $"<b>Sell</b>\n{SellProfit}g";
    }

    private void OnEnable()
    {
        if (upgradeButton == null) upgradeButton = upgradeCostText.transform.parent.GetComponent<Button>();
        if (sellButton == null) sellButton = sellProfitText.transform.parent.GetComponent<Button>();

        upgradeButton.onClick.AddListener(Upgrade);
        sellButton.onClick.AddListener(Sell);
        MoneyManager.Instance.OnMoneyAmountChange += setUpgradeButtonStatus;

        //Makes sure the text is always set correctly
        upgradeCostText.text = $"<b>Upgrade</b>\n{UpgradeCost}g";
        sellProfitText.text = $"<b>Sell</b>\n{SellProfit}g";
    }

    private void OnDisable()
    {
        upgradeButton.onClick.RemoveListener(Upgrade);
        sellButton.onClick.RemoveListener(Sell);
        MoneyManager.Instance.OnMoneyAmountChange -= setUpgradeButtonStatus;
    }

    private void setUpgradeButtonStatus(int pMoney)
    {
        if (pMoney < UpgradeCost)
            upgradeButton.interactable = false;
        else
            upgradeButton.interactable = true;
    }

    public virtual int BuyTower(Vector3 pPosition)
    {
        Instantiate(gameObject, pPosition, Quaternion.identity);

        return BuyCost;
    }

    public virtual void Upgrade()
    {
        if (MoneyManager.Instance.GetMoneyAmount() < UpgradeCost) return;

        MoneyManager.Instance.RemoveMoney(UpgradeCost);

        UpgradeCost = (int)Math.Round(UpgradeCost * UpgradeMultiplier, 0);
        Debug.Log("new upgrade cost " + UpgradeCost);

        Damage = (int)Math.Round(Damage * UpgradeMultiplier, 0);
        Range = (int)Math.Round(Range * UpgradeMultiplier, 0);
        SellProfit = (int)Math.Round(SellProfit * UpgradeMultiplier, 0);

        upgradeCostText.text = $"<b>Upgrade</b>\n{UpgradeCost}g";
        sellProfitText.text = $"<b>Sell</b>\n{SellProfit}g";
    }

    /// <summary>
    /// Sells the tower
    /// </summary>
    public virtual void Sell()
    {
        Destroy(this.gameObject);
        MoneyManager.Instance.AddMoney(SellProfit);
    }

    /// <summary>
    /// Detects if and which enemy is in range. If there is a target within range a coroutine will start which handles the shooting
    /// </summary>
    public virtual void DetectEnemyInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Range);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.tag == "Enemy")
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();

                if (enemy == null)
                    Debug.Log("Object doesn't contain Enemy component", this);
                else
                    target = enemy;
            }
        }

        if (target != null && !isShooting)
        {
            IEnumerator coroutine = Shoot(target);
            StartCoroutine(coroutine);
        }

    }

    /// <summary>
    /// Handles dealing damage to the enemy
    /// </summary>
    /// <param name="pEnemy"></param>
    /// <returns></returns>
    public virtual IEnumerator Shoot(Enemy pEnemy)
    {
        yield return new WaitForSeconds(0);
    }

    public Canvas GetButtonPanel() => upgradeBtnCanvas;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
