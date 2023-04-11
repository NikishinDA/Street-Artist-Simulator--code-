using System;
using System.Collections;
using System.Collections.Generic;
using BorderArrows;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GraffitiDrawingController : InteractableObject
{
    [SerializeField] private GameObject actionCamera;
    [SerializeField] private Transform playerHolder;
    [SerializeField] private Image graffitiImage;
    [SerializeField] private Image graffitiBack;
    [SerializeField] private Sprite[] fillSprites;
    [SerializeField] private Sprite[] backSprites;
    [Range(0.001f, 1f)] [SerializeField] private float graffitiFillAddition = 0.05f;
    [SerializeField] private bool drawLast;
    [SerializeField] private bool isFloor;
    private bool _isActive;
    private float _progress;
    [SerializeField] private CoinSpawnerController moneySpawn;

    private BorderArrow _arrow;
    protected override void Awake()
    {
        base.Awake();
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        int unlocked = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.GraffitiUnlocked);
        if (drawLast && PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.IsNewUnlocked) == 1)
        {
            unlocked--;
        }
        else
        {
            unlocked = Random.Range(0, unlocked);
        }
        graffitiImage.sprite = fillSprites[unlocked];
        graffitiBack.sprite = backSprites[unlocked];
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void Start()
    {
        if (BorderArrowFactory.Instance)
        {
            _arrow = BorderArrowFactory.Instance.CreateArrow();
            _arrow.Target = transform;
        }
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver); 
        EventManager.RemoveListener<DrawingButtonClickEvent>(OnDrawingButtonClick);
        if (_arrow)
            Destroy(_arrow.gameObject);
    }

    private void OnGameOver(GameOverEvent obj)
    {
        if (obj.IsWin && drawLast && PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.IsNewUnlocked) == 1)
            PlayerPrefs.SetInt(PlayerPrefsStrings.IsNewUnlocked.Name,0);
    }
    private void OnGameStart(GameStartEvent obj)
    {
        graffitiFillAddition = PlayerPrefsStrings.GetFloatValue(PlayerPrefsStrings.DrawingSpeed);
    }

    public override void UseObject(bool toggle)
    {
        _isActive = toggle;
        actionCamera.SetActive(_isActive);
        if ( _isActive && _playerTransform )
            StartCoroutine(SnapPlayer(0.25f));
        BroadcastDrawingToggleEvent(_isActive);
        SubscribeToDrawingButtonEvent(_isActive);
    }

    public override void UseObject()
    {
        _isActive = !_isActive;
        actionCamera.SetActive(_isActive);
        if ( _isActive && _playerTransform )
            StartCoroutine(SnapPlayer(0.25f));
        BroadcastDrawingToggleEvent(_isActive);
        SubscribeToDrawingButtonEvent(_isActive);
    }

    private void BroadcastDrawingToggleEvent(bool toggle)
    {
        var evt = GameEventsHandler.DrawingToggleEvent;
        evt.Toggle = toggle;
        evt.IsFloor = isFloor;
        EventManager.Broadcast(evt);
    }

    private void SubscribeToDrawingButtonEvent(bool toggle)
    {
        if (toggle)
            EventManager.AddListener<DrawingButtonClickEvent>(OnDrawingButtonClick);
        else
            EventManager.RemoveListener<DrawingButtonClickEvent>(OnDrawingButtonClick);
    }

    private void OnDrawingButtonClick(DrawingButtonClickEvent obj)
    {
        _progress = Mathf.Clamp01(_progress + graffitiFillAddition);
        if (_progress == 1f)
        {
            EndDraw();
        }
    }

    private void EndDraw()
    {
        DisableInteractions();
        graffitiImage.fillAmount = 1f;
        moneySpawn.SpawnMoney(isFloor);
        if (_arrow)
        {
            Destroy(_arrow.gameObject);
        }
        EventManager.Broadcast(GameEventsHandler.LevelGraffitiCompleteEvent);
    }
    private void Update()
    {
        if (_isActive)
        {
            graffitiImage.fillAmount = Mathf.MoveTowards(graffitiImage.fillAmount, _progress, Time.deltaTime);
        }
    }

    private IEnumerator SnapPlayer(float time)
    {
        Vector3 startPos = _playerTransform.position;
        Quaternion startRot = _playerTransform.rotation;
        Vector3 endPos = playerHolder.position;
        Quaternion endRot = playerHolder.rotation;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            _playerTransform.position = Vector3.Lerp(startPos, endPos, t/time);
            _playerTransform.rotation = Quaternion.Lerp(startRot, endRot, t/time);
            yield return null;
        }
        _playerTransform.position = endPos;
        _playerTransform.rotation = endRot;
    }
}
