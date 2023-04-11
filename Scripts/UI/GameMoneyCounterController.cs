using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMoneyCounterController : MonoBehaviour
{
    [SerializeField] private Text moneyText;
    private void OnEnable()
    {
        MoneyManager.MoneyCollected += MoneyManagerOnMoneyCollected; 
        moneyText.text = MoneyManager.CurrentMoney.ToString();
    }

    private void OnDisable()
    {
        MoneyManager.MoneyCollected -= MoneyManagerOnMoneyCollected;
    }

    private void MoneyManagerOnMoneyCollected()
    {
        moneyText.text = MoneyManager.CurrentMoney.ToString();
    }
}
