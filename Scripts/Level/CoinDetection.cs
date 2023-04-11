using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDetection : MonoBehaviour
{
    private Collider _trigger;
    public event Action<Transform> MoneyCollect;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    public void ToggleTrigger(bool toggle)
    {
        _trigger.enabled = toggle;
    }
    private void OnTriggerEnter(Collider other)
    {
        MoneyCollect?.Invoke(other.transform);
    }
}
