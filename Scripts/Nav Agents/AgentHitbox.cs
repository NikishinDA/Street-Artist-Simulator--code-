using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHitbox : MonoBehaviour
{
    public event Action<Transform> TargetIntercepted;

    private Collider _trigger;
    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TargetIntercepted?.Invoke(other.transform);
        var evt = GameEventsHandler.GameOverEvent;
        evt.IsWin = false;
        EventManager.Broadcast(evt);
        _trigger.enabled = false;
    }
}
