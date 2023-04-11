using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinSkinProgress : MonoBehaviour
{
    [SerializeField] private Image pbBackground;
    [SerializeField] private Image pbImage;
    [SerializeField] private float progressPerLevel = 0.4f;
    private int _skinNumber;
    [SerializeField] private Sprite[] skinsBackgrounds;
    [SerializeField] private Sprite[] skinsSprites;

    private void Awake()
    {
        _skinNumber =
            PlayerPrefs.GetInt(PlayerPrefsStrings.SkinNumber.Name, PlayerPrefsStrings.SkinNumber.DefaultValue);

        pbBackground.sprite = skinsBackgrounds[_skinNumber];
        pbImage.sprite = skinsSprites[_skinNumber];
    }
    private void Start()
    {
        StartCoroutine(SkinProgress(1f , 1f));
    }
    private IEnumerator SkinProgress(float waitTime, float time)
    {
        float weaponProgress = PlayerPrefs.GetFloat(PlayerPrefsStrings.SkinProgress.Name,
            PlayerPrefsStrings.SkinProgress.DefaultValue);
        float endProgress = weaponProgress + progressPerLevel;
        if (endProgress >= 1)
        {
            endProgress = 1;
            PlayerPrefs.SetFloat(PlayerPrefsStrings.SkinProgress.Name, 0);
            _skinNumber++;
            _skinNumber %= skinsSprites.Length;
            PlayerPrefs.SetInt(PlayerPrefsStrings.SkinNumber.Name, _skinNumber);
        }
        else
        {
            PlayerPrefs.SetFloat(PlayerPrefsStrings.SkinProgress.Name, endProgress);
        }

        pbImage.fillAmount = weaponProgress;
        yield return new WaitForSeconds(waitTime);
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            pbImage.fillAmount = Mathf.Lerp(weaponProgress, endProgress, t / time);
            yield return null;
        }
    }
}