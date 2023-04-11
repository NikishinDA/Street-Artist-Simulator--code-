using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : DistractionController
{
    [SerializeField] private GameObject effectLight;
    [SerializeField] private ParticleSystem sparkEffect;
    [SerializeField] private ParticleSystem lingeringEffect;

    protected override void ActivateEffect()
    {
        effectLight.SetActive(false);
        lingeringEffect.Play();
        sparkEffect.Play();
        Taptic.Heavy();

    }

    protected override void DeactivateEffect()
    {
        effectLight.SetActive(true);
        lingeringEffect.Stop();
    }
}
