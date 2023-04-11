using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DrawingLevel
{
    public int Cost;
    public float Speed;

    public DrawingLevel(int cost, float speed)
    {
        Cost = cost;
        Speed = speed;
    }
}
public class DrawingSpeedUpgradeManager
{
    private readonly Dictionary<int, DrawingLevel> _upgradeLevels;
    private int _currentLevel;
    public DrawingSpeedUpgradeManager()
    {
        _upgradeLevels = new Dictionary<int, DrawingLevel>
        {
            {0, new DrawingLevel(10, 0.03f)},
            {1, new DrawingLevel(20, 0.035f)},
            {2, new DrawingLevel(30, 0.04f)},
            {3, new DrawingLevel(40, 0.045f)},
            {4, new DrawingLevel(50, 0.05f)},
            {5, new DrawingLevel(55, 0.0525f)},
            {6, new DrawingLevel(60, 0.055f)},
            {7, new DrawingLevel(65, 0.0575f)},
            {8, new DrawingLevel(70, 0.06f)},
            {9, new DrawingLevel(75, 0.0625f)},
            {10, new DrawingLevel(80, 0.065f)},
            {11, new DrawingLevel(85, 0.0675f)},
            {12, new DrawingLevel(90, 0.07f)},
            {13, new DrawingLevel(95, 0.0725f)},
            {14, new DrawingLevel(100, 0.075f)},
            {15, new DrawingLevel(105, 0.0775f)},
            {16, new DrawingLevel(110, 0.08f)},
            {17, new DrawingLevel(115, 0.0825f)},
            {18, new DrawingLevel(120, 0.085f)},
            {19, new DrawingLevel(125, 0.0875f)},
            {20, new DrawingLevel(9999, 0.09f)}
        };

        _currentLevel = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.DrawingUpgradeLevel);
        _currentLevel = Mathf.Clamp(_currentLevel, 0, _upgradeLevels.Count);
    }

    public int? GetCurrentCost()
    {
        if (_currentLevel == _upgradeLevels.Count)
            return null;
        return _upgradeLevels[_currentLevel].Cost;
    }

    public float GetCurrentSpeed()
    {
        return _upgradeLevels[_currentLevel].Speed;
    }

    public void Upgrade()
    {
        _currentLevel = Mathf.Clamp(_currentLevel + 1, 0, _upgradeLevels.Count);
        PlayerPrefs.SetInt(PlayerPrefsStrings.DrawingUpgradeLevel.Name, _currentLevel);
        PlayerPrefs.SetFloat(PlayerPrefsStrings.DrawingSpeed.Name,_upgradeLevels[ _currentLevel].Speed);
        PlayerPrefs.Save();
    }
}
