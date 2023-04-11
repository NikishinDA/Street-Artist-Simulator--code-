using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHurtbox : MonoBehaviour
{
    public event Action<Transform> NoiseHeard;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void ToggleCollider(bool toggle)
    {
        _collider.enabled = toggle;
    }
    public void Distract(Transform distractionTransform)
    {
        NoiseHeard?.Invoke(distractionTransform);
    }
    
}
