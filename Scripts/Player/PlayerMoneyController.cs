using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyController : MonoBehaviour
{
    [SerializeField] private int coinCost;
    [SerializeField] private float collectionRadius;
    [SerializeField] private LayerMask moneyLayer;
    [SerializeField] private float winCollectDelay;
    private void Awake()
    {
        EventManager.AddListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        MoneyManager.Reset();
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);

    }

    private void OnGameOver(GameOverEvent obj)
    {
        if (obj.IsWin)
        {
            MoneyManager.AddMoneyToTotal();
            StartCoroutine(CollectMoneyCor(winCollectDelay));
        }
    }

    private void OnMoneyCollect(MoneyCollectEvent obj)
    {
        MoneyManager.CollectMoney(coinCost);
        Taptic.Medium();
    }

    private IEnumerator CollectMoneyCor(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        CollectAround();
    }
    private void CollectAround()
    {
        Collider[] surroundMoney = Physics.OverlapSphere(transform.position, collectionRadius, moneyLayer);
        foreach (var money in surroundMoney)
        {
            money.GetComponent<CoinController>().CollectArtificially(transform);
        }
    }
}
