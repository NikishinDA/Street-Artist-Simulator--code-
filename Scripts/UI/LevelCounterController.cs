using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounterController : MonoBehaviour
{
    [SerializeField] private Text levelText;
    private void Awake()
    {
        int level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.LevelFixed);
        levelText.text = level + " LVL";
    }
}
