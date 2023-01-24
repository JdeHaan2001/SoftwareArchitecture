using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private BaseManager baseManager;
    [SerializeField]
    private GameObject loseText;
    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private TextMeshProUGUI moneyChangeText;
    [SerializeField]
    private float waitTimeMoneyChangeText = 3f;
    [SerializeField]
    private TextMeshProUGUI waveNumberText;

    private int moneyAmount = 0;
    private IEnumerator moneyChangeCoroutine;

    private void Start()
    {
        loseText.SetActive(false);
        //Very ugly fix for a null pointer
        //Put this here because the onEnable function gets called earlier then the awake of the Moneymanager
        //So if this isn't here then the instance of MoneyManager will be null
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.OnMoneyAmountChange += updateMoneyText;
    }

    private void OnEnable()
    {
        baseManager.OnLoseGame += showLoseText;

        if(MoneyManager.Instance != null)
            MoneyManager.Instance.OnMoneyAmountChange += updateMoneyText;
    }

    private void OnDisable()
    {
        baseManager.OnLoseGame -= showLoseText;
        MoneyManager.Instance.OnMoneyAmountChange -= updateMoneyText;
    }

    private void showLoseText() => loseText.SetActive(true);

    private void updateMoneyText(int pMoney)
    {
        int moneyChangeAmount = pMoney - moneyAmount;
        moneyAmount = pMoney;
        moneyText.text = pMoney.ToString();

        moneyChangeCoroutine = moneyChange(moneyChangeAmount);

        StartCoroutine(moneyChangeCoroutine);
    }

    private IEnumerator moneyChange(int pMoney)
    {
        if (pMoney < 0)
            moneyChangeText.text = $"-{pMoney * -1} gold";
        else
            moneyChangeText.text = $"+{pMoney} gold";

        moneyChangeText.gameObject.SetActive(true);

        yield return new WaitForSeconds(waitTimeMoneyChangeText);

        moneyChangeText.gameObject.SetActive(false);

    }

    public void UpdateWaveNumberText(int pWaveNumber)
    {
        waveNumberText.text = $"Wave {pWaveNumber}";
    }
}
