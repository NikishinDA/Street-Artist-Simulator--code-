using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseDetection : MonoBehaviour
{
    public event Action<bool, Transform> ActiveElement;
    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ActiveElement?.Invoke(true, other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        ActiveElement?.Invoke(false, null);
    }

    public void Enable()
    {
        _trigger.enabled = true;
    }

    public void Disable()
    {
        _trigger.enabled = false;
        ActiveElement?.Invoke(false, null);
    }
}
