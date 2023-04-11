using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseButtonController : MonoBehaviour
{
    [SerializeField] private Button useButton;
    private InteractType _currentType = InteractType.None;
    private bool _toggle;
    [SerializeField] private Sprite drawActionButtonSprite;
    [SerializeField] private Sprite distractActionButtonSprite;
    [SerializeField] private Sprite lookActionButtonSprite;
    [SerializeField] private Sprite closeActionButtonSprite;
    private void Awake()
    {
        EventManager.AddListener<UseButtonToggleEvent>(OnToggleUseButton);
        EventManager.AddListener<UseButtonClickEvent>(OnUseButtonClick);
        useButton.onClick.AddListener(UseButtonOnClick);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<UseButtonToggleEvent>(OnToggleUseButton);
        EventManager.RemoveListener<UseButtonClickEvent>(OnUseButtonClick);

    }

    private void OnUseButtonClick(UseButtonClickEvent obj)
    {
        _toggle = !_toggle;
        if (_toggle)
        {
            //actionText.text = "X";
            useButton.image.sprite = closeActionButtonSprite;
        }
        else
        {
            SwitchText(_currentType);
        }
    }

    private void OnToggleUseButton(UseButtonToggleEvent obj)
    {
        _toggle = false;
        useButton.gameObject.SetActive(obj.Toggle);
        _currentType = obj.Type;
        SwitchText(obj.Type);
    }

    private void SwitchText(InteractType type)
    {
        switch (type)
        {
            case InteractType.None:
                useButton.image.sprite = closeActionButtonSprite;
                break;
            case InteractType.Graffiti:
                useButton.image.sprite = drawActionButtonSprite;
                break;
            case InteractType.Distraction:
                useButton.image.sprite = distractActionButtonSprite;
                break;
            case InteractType.New:
                useButton.image.sprite = lookActionButtonSprite;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UseButtonOnClick()
    {
        EventManager.Broadcast(GameEventsHandler.UseButtonClickEvent);
    }
}
