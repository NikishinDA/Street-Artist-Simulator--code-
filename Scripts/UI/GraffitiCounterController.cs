using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraffitiCounterController : MonoBehaviour
{
    [SerializeField] private int graffitiNumber;
    [SerializeField] private Text counterText;
    private int _completeNumber;
    private void Awake()
    {
        EventManager.AddListener<LevelGraffitiCompleteEvent>(OnGraffitiComplete);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<LevelGraffitiCompleteEvent>(OnGraffitiComplete);

    }

    private void Start()
    {
        UpdateText();
    }

    private void OnGraffitiComplete(LevelGraffitiCompleteEvent obj)
    {
        _completeNumber++;
        UpdateText();
    }

    private void UpdateText()
    {
        counterText.text = _completeNumber + "/" + graffitiNumber;
    }
}
