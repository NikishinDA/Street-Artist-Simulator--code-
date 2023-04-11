using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool _isMoving;
    private bool _isDrawing;

    [SerializeField] private float drawCooldown;
    private float _cooldown;
    private static readonly int s_stopDraw = Animator.StringToHash("Stop Draw");
    private static readonly int s_takeOut = Animator.StringToHash("Take Out");
    private static readonly int s_draw = Animator.StringToHash("Draw");
    private static readonly int s_moving = Animator.StringToHash("Moving");
    private static readonly int s_caught = Animator.StringToHash("Caught");
    private static readonly int s_floor = Animator.StringToHash("Floor");
    private static readonly int s_win = Animator.StringToHash("Win");

    private void Awake()
    {
        EventManager.AddListener<DrawingToggleEvent>(OnDrawingToggle);
        EventManager.AddListener<DrawingButtonClickEvent>(OnDrawingButtonClick);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<DrawingToggleEvent>(OnDrawingToggle);
        EventManager.RemoveListener<DrawingButtonClickEvent>(OnDrawingButtonClick);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);

    }

    public void SetAnimator(Animator animtr)
    {
        animator = animtr;
    }
    private void OnGameOver(GameOverEvent obj)
    {
        if (!obj.IsWin)
        {
            animator.SetTrigger(s_caught);
            Taptic.Failure();
        }
        else
        {
            animator.SetTrigger(s_win);
        }
    }

    private void OnDrawingButtonClick(DrawingButtonClickEvent obj)
    {
        if (!_isDrawing)
            animator.SetBool(s_draw, true);
        _cooldown = drawCooldown;
        _isDrawing = true;
    }

    private void OnDrawingToggle(DrawingToggleEvent obj)
    {            
        animator.ResetTrigger(s_stopDraw);

        if (obj.Toggle)
        {
            animator.SetTrigger(s_takeOut);
            animator.SetBool(s_floor, obj.IsFloor);
        }
        else
        {
            animator.SetTrigger(s_stopDraw);
        }

        _isDrawing = obj.Toggle;
    }

    private void Update()
    {
        if (_isDrawing)
            if (_cooldown > 0)
            {
                _cooldown -= Time.deltaTime;
            }
            else
            {
                _isDrawing = false;
                animator.SetBool(s_draw, false);
            }
    }

    public void SetMoving(bool isMove)
    {
        if (isMove && _isMoving || !isMove && !_isMoving)
            return;
        _isMoving = isMove;
        animator.SetBool(s_moving, _isMoving);
    }
    
}
