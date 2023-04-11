using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDetection : MonoBehaviour
{
    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var evt = GameEventsHandler.GameOverEvent;
        evt.IsWin = true;
        EventManager.Broadcast(evt);
        _trigger.enabled = false;
    }
}
