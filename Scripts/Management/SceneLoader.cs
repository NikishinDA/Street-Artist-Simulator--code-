using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextLevel()
    {
        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level.Name, PlayerPrefsStrings.Level.DefaultValue);
        PlayerPrefs.SetInt(PlayerPrefsStrings.Level.Name, level + 1);
        int levelFixed = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.LevelFixed);
        PlayerPrefs.SetInt(PlayerPrefsStrings.LevelFixed.Name, levelFixed + 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public static void LoadPreviousLevel()
    {
        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level.Name, PlayerPrefsStrings.Level.DefaultValue);
        if (level > 1)
        {
            PlayerPrefs.SetInt(PlayerPrefsStrings.Level.Name, level - 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
        }
    }
}
