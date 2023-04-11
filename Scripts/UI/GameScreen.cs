using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private GameObject useButtonCanvas;
    [SerializeField] private GameObject drawButtonCanvas;
    [SerializeField] private GameObject joystickCanvas;
    [SerializeField] private GameObject newGraffitiTextCanvas;
    private void Awake()
    {
        //EventManager.AddListener<UseButtonToggleEvent>(OnToggleUseButton);
        EventManager.AddListener<DrawingToggleEvent>(OnDrawingToggle);
        EventManager.AddListener<NewGraffitiShowEvent>(OnNewGraffitiShow);
    }

    private void OnDestroy()
    {
        //EventManager.RemoveListener<UseButtonToggleEvent>(OnToggleUseButton);
        EventManager.RemoveListener<DrawingToggleEvent>(OnDrawingToggle);
        EventManager.RemoveListener<NewGraffitiShowEvent>(OnNewGraffitiShow);

    }

    private void OnNewGraffitiShow(NewGraffitiShowEvent obj)
    {
        joystickCanvas.SetActive(!obj.Toggle);
        newGraffitiTextCanvas.SetActive(obj.IsFirst);
    }

    private void OnDrawingToggle(DrawingToggleEvent obj)
    {
        drawButtonCanvas.SetActive(obj.Toggle);
        joystickCanvas.SetActive(!obj.Toggle);
    }

    private void OnToggleUseButton(UseButtonToggleEvent obj)
    {
        useButtonCanvas.SetActive(obj.Toggle);
    }
}
