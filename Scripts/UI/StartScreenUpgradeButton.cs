using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenUpgradeButton : MonoBehaviour
{
    private DrawingSpeedUpgradeManager _upgradeManager;
    [SerializeField] private Text upgradeText;
    [SerializeField] private float costCorrection = 1f;

    [SerializeField] private Button upgradeButton;
    private int? _currentCost;
    private void Awake()
    {
        _upgradeManager = new DrawingSpeedUpgradeManager();
        UpdateText();
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }

    private void OnUpgradeButtonClick()
    {        
        _currentCost = _upgradeManager.GetCurrentCost();
        if (_currentCost == null || !MoneyManager.TrySpend((int) _currentCost)) return;
        _upgradeManager.Upgrade();
        UpdateText();
        Taptic.Heavy();
    }

    private void UpdateText()
    {
        _currentCost = _upgradeManager.GetCurrentCost();
        if (_currentCost != null)
        {
            upgradeText.text = ((int) _currentCost * costCorrection).ToString("N0");
        }
        else
        {
            upgradeText.text = "";
        }
    }
}
