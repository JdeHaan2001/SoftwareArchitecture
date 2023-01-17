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
    private TextMeshProUGUI MoneyText;

    private void Start()
    {
        loseText.SetActive(false);
    }

    private void OnEnable()
    {
        baseManager.OnLoseGame += showLoseText;
        MoneyManager.Instance.OnMoneyAmountChange += updateMoneyText;
    }

    private void OnDisable()
    {
        baseManager.OnLoseGame -= showLoseText;
        MoneyManager.Instance.OnMoneyAmountChange -= updateMoneyText;
    }

    private void showLoseText() => loseText.SetActive(true);

    private void updateMoneyText(int pMoney) => MoneyText.text = pMoney.ToString();
}
