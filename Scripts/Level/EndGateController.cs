using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGateController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Collider finishTrigger;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        EventManager.AddListener<LevelObjectivesCompleteEvent>(OnObjectivesComplete);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<LevelObjectivesCompleteEvent>(OnObjectivesComplete);
    }

    private void OnObjectivesComplete(LevelObjectivesCompleteEvent obj)
    {
        _animator.SetBool("Open", true);
        finishTrigger.enabled = true;
    }
}
