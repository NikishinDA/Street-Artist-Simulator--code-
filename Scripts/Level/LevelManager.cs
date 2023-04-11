using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int graffitiNumber;
    private int _completedGraffiti;
    private bool _isActive = true;
    private void Awake()
    {
        EventManager.AddListener<LevelGraffitiCompleteEvent>(OnGraffitiDrawn);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<LevelGraffitiCompleteEvent>(OnGraffitiDrawn);
    }

    private void OnGraffitiDrawn(LevelGraffitiCompleteEvent obj)
    {
        if (!_isActive) return;
        _completedGraffiti++;
        if (_completedGraffiti >= graffitiNumber)
        {
            //EventManager.Broadcast(GameEventsHandler.LevelObjectivesCompleteEvent);
            var evt = GameEventsHandler.GameOverEvent;
            evt.IsWin = true;
            EventManager.Broadcast(evt);
            _isActive = false;
        }
    }
}
