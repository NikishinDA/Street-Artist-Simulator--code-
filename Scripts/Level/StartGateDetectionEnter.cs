using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGateDetectionEnter : MonoBehaviour
{
    public event Action OpenGates;
    private Collider _trigger;

    public void ToggleTrigger(bool toggle)
    {
        _trigger.enabled = toggle;
    }
    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        OpenGates?.Invoke();
    }
}
