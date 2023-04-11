using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreference<T>
{
    public string Name;
    public T DefaultValue;
}
public class PlayerPrefsStrings
{
    public static readonly PlayerPreference<int> Level = new PlayerPreference<int> {Name = "Level", DefaultValue = 1};
public static readonly PlayerPreference<int> TimesRotated = new PlayerPreference<int> {Name = "TimesRotated", DefaultValue = 0};
    public static readonly PlayerPreference<float> SkinProgress = new PlayerPreference<float>
        {Name = "SkinProgress", DefaultValue = 0.01f};
    public static readonly PlayerPreference<int> SkinNumber = new PlayerPreference<int> {Name = "SkinNumber", DefaultValue = 0};
    public static readonly PlayerPreference<int> SkinsUnlocked = new PlayerPreference<int> {Name = "SkinsUnlocked", DefaultValue = 0};
    public static readonly PlayerPreference<float> DrawingSpeed = new PlayerPreference<float>
    {
        Name = "DrawingSpeed", DefaultValue = 0.03f
    };
    public static readonly PlayerPreference<int> DrawingUpgradeLevel = new PlayerPreference<int>
    {
        Name = "DrawingUpgradeLevel", DefaultValue = 0
    };
    public static readonly PlayerPreference<int> MoneyTotal = new PlayerPreference<int>
    {
        Name = "MoneyTotal", DefaultValue = 0
    };
    public static readonly PlayerPreference<int> GraffitiUnlocked = new PlayerPreference<int>
    {
        Name = "GraffitiUnlocked", DefaultValue = 2
    };

    public static readonly PlayerPreference<int> IsNewUnlocked = new PlayerPreference<int>
    {
        Name = "IsNewUnlocked", DefaultValue = 0
    };

    public static readonly PlayerPreference<int> LevelFixed = new PlayerPreference<int>()
    {
        Name = "LevelFixed", DefaultValue = 1
    };
    public static int GetIntValue(PlayerPreference<int> preference)
    {
        return PlayerPrefs.GetInt(preference.Name, preference.DefaultValue);
    }

    public static float GetFloatValue(PlayerPreference<float> preference)
    {
        return PlayerPrefs.GetFloat(preference.Name, preference.DefaultValue);
    }
}
