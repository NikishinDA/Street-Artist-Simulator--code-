using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGateController : MonoBehaviour
{

    [SerializeField] private StartGateDetectionEnter detectionEnter;
    [SerializeField] private StartGateDetectionExit detectionExit;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        detectionEnter.OpenGates += DetectionEnterOnOpenGates;
        detectionExit.CloseGates += DetectionExitOnCloseGates;
    }

    private void DetectionExitOnCloseGates()
    {
        _animator.SetBool("Open", false);
        //detectionEnter.ToggleTrigger(true);
        detectionExit.ToggleTrigger(false);
    }

    private void DetectionEnterOnOpenGates()
    {
        _animator.SetBool("Open", true);
        //detectionEnter.ToggleTrigger(false);
        detectionExit.ToggleTrigger(true);
    }
}
