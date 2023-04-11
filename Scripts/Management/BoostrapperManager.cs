using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoostrapperManager : MonoBehaviour
{
    //[SerializeField] private Image pbFill;
    [SerializeField] private Image loadFill;
    [SerializeField] private float artificialDelay = 1f;
    private float _progress;
    private void Awake()
    {
        StartCoroutine(LoadScene(PlayerPrefs.GetInt(PlayerPrefsStrings.Level.Name,
            PlayerPrefsStrings.Level.DefaultValue)));
    }

    private IEnumerator LoadScene(int num)
    {
        yield return null;
        num %= SceneManager.sceneCountInBuildSettings;
        if (num == 0)
        {
            num = 2;
            PlayerPrefs.SetInt(PlayerPrefsStrings.TimesRotated.Name,
                PlayerPrefs.GetInt(PlayerPrefsStrings.TimesRotated.Name, PlayerPrefsStrings.TimesRotated.DefaultValue) + 1);
            int level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level.Name, PlayerPrefsStrings.Level.DefaultValue);
            PlayerPrefs.SetInt(PlayerPrefsStrings.Level.Name, level +  num);
            PlayerPrefs.Save();
        }
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(num);
        sceneLoading.allowSceneActivation = false;
        float loadTime = 0;
        while (!sceneLoading.isDone)
        {
            loadTime += Time.deltaTime;
            loadFill.fillAmount = sceneLoading.progress;
            if (sceneLoading.progress >= 0.9f)
            {
                for (float t = loadTime; t < artificialDelay; t += Time.deltaTime)
                {
                    loadFill.fillAmount = Mathf.Lerp(0.9f, 1, t / artificialDelay);
                    yield return null;
                }
                loadFill.fillAmount = 1f;

                sceneLoading.allowSceneActivation = true;
                yield break;
            } 
            yield return null;
        }
       /* Debug.Log("2");

        for (float t = loadTime; t < artificialDelay; t += Time.deltaTime)
        {
            yield return null;
        }
        Debug.Log("3");

        sceneLoading.allowSceneActivation = true;*/
    }
}
