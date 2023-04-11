using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGraffitiController : InteractableObject
{
    [SerializeField] private GameObject actionCamera;
    private bool _isActive;
    private bool _firstActivation = true;
    [SerializeField] private string saveName;
    [SerializeField] private ParticleSystem[] unlockEffects;

    protected override void Awake()
    {
        if (string.IsNullOrEmpty(saveName))
        {
            throw new Exception("New graffiti does not have a save name");
        }

        base.Awake();
        if (PlayerPrefs.GetInt(saveName, 0) == 1)
            _firstActivation = false;
    }

    public override void UseObject(bool toggle)
    {
        if (toggle)
        {
            UseObject();
        }
    }

    public override void UseObject()
    {
        _isActive = !_isActive;
        actionCamera.SetActive(_isActive);
        BroadcastShowEvent(_isActive);
        if (_firstActivation)
        {
            SaveGraffiti();
            foreach (var effect in unlockEffects)
            {
                effect.Play();
            }
        }

        _firstActivation = false;
    }

    private void BroadcastShowEvent(bool toggle)
    {
        var evt = GameEventsHandler.NewGraffitiShowEvent;
        evt.Toggle = toggle;
        evt.IsFirst = _firstActivation;
        EventManager.Broadcast(evt);
    }

    private void SaveGraffiti()
    {
        PlayerPrefs.SetInt(saveName, 1);
        PlayerPrefs.SetInt(PlayerPrefsStrings.GraffitiUnlocked.Name,
            PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.GraffitiUnlocked) + 1);
        PlayerPrefs.SetInt(PlayerPrefsStrings.IsNewUnlocked.Name, 1);
        PlayerPrefs.Save();
    }
}