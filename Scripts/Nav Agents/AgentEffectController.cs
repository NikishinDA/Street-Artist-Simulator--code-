using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentEffectController : MonoBehaviour
{
    [SerializeField] private Renderer coneRenderer;
    [SerializeField] private Color patrolColor;
    [SerializeField] private Color alertColor;
    [SerializeField] private Color chaseColor;

    [SerializeField] private ParticleSystem exclamationEffect;
    [SerializeField] private ParticleSystem questionEffect;

    
    public void ColorPatrol()
    {
        coneRenderer.material.color = patrolColor;
    }

    public void ColorAlert()
    {
        coneRenderer.material.color = alertColor;
    }

    public void ColorChase()
    {
        coneRenderer.material.color = chaseColor;
    }

    public void AlertEffect()
    {
        questionEffect.Play();
        Taptic.Success();
    }

    public void ChaseEffect()
    {
        exclamationEffect.Play();
        Taptic.Warning();
    }
}
