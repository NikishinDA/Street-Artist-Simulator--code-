using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawButtonController : MonoBehaviour
{
    [SerializeField] private Button drawButton;

    private void Awake()
    {
        drawButton.onClick.AddListener(OnDrawButtonClick);
    }


    private void OnDrawButtonClick()
    {
        EventManager.Broadcast(GameEventsHandler.DrawingButtonClickEvent);
    }
}
