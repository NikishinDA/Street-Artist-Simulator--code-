using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Joystick joystick;

    [SerializeField] private float speed;

    [SerializeField] private PlayerAnimationController animationController;
    private CharacterController _cc;
    private Vector3 _moveVector;
    private Quaternion _lookRot;
    private bool _isActive;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        
        EventManager.AddListener<DrawingToggleEvent>(OnDrawingToggle);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<DrawingToggleEvent>(OnDrawingToggle);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);

    }

    private void OnGameStart(GameStartEvent obj)
    {
        _isActive = true;
    }

    private void OnGameOver(GameOverEvent obj)
    {
        _isActive = false;
    }

    private void OnDrawingToggle(DrawingToggleEvent obj)
    {
        _isActive = !obj.Toggle;
    }

    private void Update()
    {
        if (_isActive)
        {
            _moveVector.x = joystick.Horizontal;
            _moveVector.z = joystick.Vertical;
                animationController.SetMoving(_moveVector != Vector3.zero);
                Rotate(_moveVector);
            _cc.Move(_moveVector * (speed * Time.deltaTime));
        }
    }

    private void Rotate(Vector3 look)
    {
        if (look != Vector3.zero)
        {
            _lookRot = Quaternion.LookRotation(look);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, _lookRot, 360 * Time.deltaTime);
    }
}