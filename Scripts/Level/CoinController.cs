using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private CoinDetection detection;
    [SerializeField] private float activationDelay = 1.5f;
    [SerializeField] private Transform coinModelTransform;
    [SerializeField] private float flyTime;
    private void Awake()
    {
        detection.MoneyCollect += DetectionOnMoneyCollect;
    }

    private void Start()
    {
        StartCoroutine(EnableDetectionCor(activationDelay));
    }

    private void DetectionOnMoneyCollect(Transform playerTransform)
    {
        detection.ToggleTrigger(false);
        StartCoroutine(FlyCor(flyTime, playerTransform));
    }

    private IEnumerator EnableDetectionCor(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        detection.ToggleTrigger(true);
    }

    public void CollectArtificially(Transform playerTransform)
    {
        detection.ToggleTrigger(false);
        StartCoroutine(FlyCor(flyTime, playerTransform));
    }
    private IEnumerator FlyCor(float time, Transform playerTransform)
    {
        Vector3 startPos = coinModelTransform.position;
        Vector3 startScale = coinModelTransform.localScale;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            coinModelTransform.position = Vector3.Lerp(startPos, playerTransform.position, t/time);
            coinModelTransform.localScale = Vector3.Lerp(startScale, Vector3.zero, t /time);
            yield return null;
        }
        
        EventManager.Broadcast(GameEventsHandler.MoneyCollectEvent);
        gameObject.SetActive(false);
    }
}
