using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum InteractType
{
    None = -1,
    Graffiti,
    Distraction,
    New,
    GraffitiFloor
}

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] protected UseDetection useDetection;
    [SerializeField] private InteractType interactType;
    protected Transform _playerTransform;

    public InteractType InteractType => interactType;

    protected virtual void Awake()
    {
        useDetection.ActiveElement += UseDetectionOnActiveElement;
    }

    private void UseDetectionOnActiveElement(bool obj, Transform playerTransform)
    {
        _playerTransform = playerTransform;
        BroadcastActivateEvent(obj);
    }

    private void BroadcastActivateEvent(bool obj)
    {
        var evt = GameEventsHandler.InteractableObjectActivateEvent;
        evt.Object = this;
        evt.IsActive = obj;
        EventManager.Broadcast(evt);
    }

    protected void EnableInteractions()
    {
        useDetection.Enable();
    }
    protected void DisableInteractions()
    {
        UseObject(false);
        BroadcastActivateEvent(false);
        useDetection.Disable();
    }

   /* private void OnUseButtonClick(UseButtonClickEvent obj)
    {
        UseObject();
    }*/

    public abstract void UseObject(bool toggle);
    public abstract void UseObject();
}
