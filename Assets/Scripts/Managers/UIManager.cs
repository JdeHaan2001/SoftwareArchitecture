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

    private void Start()
    {
        loseText.SetActive(false);
    }

    private void OnEnable()
    {
        baseManager.OnLoseGame += showLoseText;
    }

    private void OnDisable()
    {
        baseManager.OnLoseGame -= showLoseText;
    }

    private void showLoseText() => loseText.SetActive(true);
}
