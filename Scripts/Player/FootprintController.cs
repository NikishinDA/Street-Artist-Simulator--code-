using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintController : MonoBehaviour
{
    [SerializeField] private GameObject footprint;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Color _startColor;
    private Color _endColor;
    public Transform PlayerRef
    {
        get;
        private set;
    }

    private void Awake()
    {
        _startColor = spriteRenderer.color;
        _endColor = _startColor;
        _endColor.a = 0;
    }

    public void Initialize(float lifeTime, FootprintsPool pool, Transform playerTransform)
    {
        StartCoroutine(LifeTimeCor(lifeTime, pool));
        footprint.SetActive(true);
        if (!PlayerRef) PlayerRef = playerTransform;
    }

    private IEnumerator LifeTimeCor(float time, FootprintsPool pool)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            spriteRenderer.color = Color.Lerp(_startColor, _endColor, t / time);
            yield return null;
        }
        footprint.SetActive(false);
        pool.AddToPool(this);
    }
}
