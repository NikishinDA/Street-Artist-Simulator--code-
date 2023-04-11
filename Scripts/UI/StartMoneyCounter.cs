using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMoneyCounter : MonoBehaviour
{
    [SerializeField] private Text moneyText;

    private void OnEnable()
    {
        MoneyManager.MoneySpend += MoneyManagerOnMoneySpend;
    }
    private void OnDisable()
    {
        MoneyManager.MoneySpend -= MoneyManagerOnMoneySpend;
    }

    private void MoneyManagerOnMoneySpend()
    {
        moneyText.text = MoneyManager.CurrentMoney.ToString();
    }
    private void Start()
    {
        moneyText.text = MoneyManager.CurrentMoney.ToString();
    }
    
}
