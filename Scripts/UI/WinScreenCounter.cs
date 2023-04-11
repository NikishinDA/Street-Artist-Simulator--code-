using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenCounter : MonoBehaviour
{
    [SerializeField] private Text moneyText;

    private int _collectedMoney;
    private void Start()
    {
        _collectedMoney = MoneyManager.CollectedMoney;
        StartCoroutine(MoneyCounterCor(1f));
    }

    private IEnumerator MoneyCounterCor(float time)
    {
        moneyText.text = "0";
        int money = 0;
        yield return new WaitForSeconds(1f);
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            moneyText.text = money.ToString();
            money = (int) Mathf.Lerp(0, _collectedMoney, t / time);
            yield return null;
        }

        moneyText.text = _collectedMoney.ToString();
    }
}
