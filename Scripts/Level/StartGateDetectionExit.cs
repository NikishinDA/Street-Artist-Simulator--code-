using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGateDetectionExit : MonoBehaviour
{
    public event Action CloseGates;
    private Collider _trigger;

    public void ToggleTrigger(bool toggle)
    {
        _trigger.enabled = toggle;
    }
    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }
    
    private void OnTriggerExit(Collider other)
    {
        CloseGates?.Invoke();
    }
}
