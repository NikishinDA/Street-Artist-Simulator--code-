using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }

    private void OnGameOver(GameOverEvent obj)
    {
        gameScreen.SetActive(false);
        if (obj.IsWin)
        {
            StartCoroutine(WaitCor(3f, winScreen));

        }
        else
        {
            StartCoroutine(WaitCor(3f, loseScreen));
        }
    }

    private void OnGameStart(GameStartEvent obj)
    {
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    private void Start()
    {
        startScreen.SetActive(true);
    }

    private IEnumerator WaitCor(float time, GameObject screen)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        screen.SetActive(true);
    }
    
}
