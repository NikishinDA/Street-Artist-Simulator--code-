using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneyManager
{
    private static int s_currentMoney;
    private static int s_collectedMoney;

    public static void Reset()
    {
        s_currentMoney = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.MoneyTotal);
        s_collectedMoney = 0;
    }
    public static int CurrentMoney => s_currentMoney;

    public static int CollectedMoney => s_collectedMoney;

    public static event Action MoneyCollected;
    public static event Action MoneySpend;
    public static bool TrySpend(int cost)
    {
        if (s_currentMoney >= cost)
        {
            s_currentMoney -= cost;
            PlayerPrefs.SetInt(PlayerPrefsStrings.MoneyTotal.Name, s_currentMoney);
            PlayerPrefs.Save();
            MoneySpend?.Invoke();
            return true;
        }

        return false;
    }

    public static void CollectMoney(int amount)
    {
        s_collectedMoney += amount;
        s_currentMoney += amount;
        MoneyCollected?.Invoke();
    }

    public static void AddMoneyToTotal()
    {
        PlayerPrefs.SetInt(PlayerPrefsStrings.MoneyTotal.Name, s_currentMoney);
        PlayerPrefs.Save();
    }
}
