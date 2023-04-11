using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DistractionController : InteractableObject
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private float cooldown;
    //[SerializeField] private DistractionEffectController effectController;
    //[SerializeField] private UseDetection useDetection;

    public override void UseObject(bool toggle)
    {
        if (toggle)
        {
            UseObject();
        }
    }

    public override void UseObject()
    {
        Collider[] guardColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        foreach (var guardCollider in guardColliders)
        {
            guardCollider.GetComponent<AgentHurtbox>().Distract(transform);
        }
        ActivateEffect();
        StartCoroutine(CooldownCor(cooldown));
    }

    private IEnumerator CooldownCor(float time)
    {
        DisableInteractions();
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        EnableInteractions();
        DeactivateEffect();
    }

    protected abstract void ActivateEffect();
    protected abstract void DeactivateEffect();
}
