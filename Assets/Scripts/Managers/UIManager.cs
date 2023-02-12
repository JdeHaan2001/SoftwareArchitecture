using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
    [SerializeField]
    private TextMeshProUGUI timeToBuildText;

    private WaveManager waveManager = null;

    private int moneyAmount = 0;
    private IEnumerator moneyChangeCoroutine;

    private void Start()
    {
        loseText.SetActive(false);
        timeToBuildText.gameObject.SetActive(false);
        waveManager = GetComponent<WaveManager>();
        waveManager.OnIsWaveActiveChange += SetHandleTimeToBuildTimer;
        waveManager.OnBuildTimeChange += updateBuildTimer;

        //Very ugly fix for a null pointer error
        //Put this here because the onEnable function gets called earlier then the awake of the Moneymanager
        //So if this isn't here then the instance of MoneyManager will be null
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.OnMoneyAmountChange += updateMoneyText;
    }

    private void OnEnable()
    {
        if(MoneyManager.Instance != null)
            MoneyManager.Instance.OnMoneyAmountChange += updateMoneyText;
        if (waveManager != null)
        {
            waveManager.OnIsWaveActiveChange += SetHandleTimeToBuildTimer;
            waveManager.OnBuildTimeChange += updateBuildTimer;
        }
    }

    private void OnDisable()
    {
        MoneyManager.Instance.OnMoneyAmountChange -= updateMoneyText;
        if (waveManager != null)
        {
            waveManager.OnIsWaveActiveChange -= SetHandleTimeToBuildTimer;
            waveManager.OnBuildTimeChange -= updateBuildTimer;
        }
    }

    private void SetHandleTimeToBuildTimer(bool pIsWaveActive)
    {
        timeToBuildText.gameObject.SetActive(!pIsWaveActive);
    }

    private void updateBuildTimer(int pTime)
    {
        timeToBuildText.text = $"Time to build:\n{pTime}";
    }

    private void updateMoneyText(int pMoney)
    {
        int moneyChangeAmount = pMoney - moneyAmount;
        moneyAmount = pMoney;
        moneyText.text = pMoney.ToString();

        if (moneyChangeAmount == 0) return;
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
