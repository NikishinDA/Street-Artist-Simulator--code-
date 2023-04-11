using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingSubject : MonoBehaviour
{
    private InteractableObject _currentObject;

    private void Awake()
    {
        EventManager.AddListener<UseButtonClickEvent>(OnUseButtonClick);
        EventManager.AddListener<InteractableObjectActivateEvent>(OnInteractableObjectActivate);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<UseButtonClickEvent>(OnUseButtonClick);
        EventManager.RemoveListener<InteractableObjectActivateEvent>(OnInteractableObjectActivate);
    }

    private void OnInteractableObjectActivate(InteractableObjectActivateEvent obj)
    {
        if (!obj.IsActive && _currentObject != obj.Object) return;
        if (_currentObject)
        {
            _currentObject.UseObject(false);
        }

        if (obj.IsActive)
        {
            _currentObject = obj.Object;
            BroadcastUseToggleEvent(obj.IsActive, _currentObject.InteractType);
        }
        else
        {
            _currentObject = null;
            BroadcastUseToggleEvent(obj.IsActive, InteractType.None);
        }
    }

    private void OnUseButtonClick(UseButtonClickEvent obj)
    {
        if (_currentObject)
            _currentObject.UseObject();
        else
        {
            Debug.LogError("Use Button on Null object");
        }
    }

    private void BroadcastUseToggleEvent(bool toggle, InteractType type)
    {
        var evt = GameEventsHandler.UseButtonToggleEvent;
        evt.Toggle = toggle;
        evt.Type = type;
        EventManager.Broadcast(evt);
    }
}