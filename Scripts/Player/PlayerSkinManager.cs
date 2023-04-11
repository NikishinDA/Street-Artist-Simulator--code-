using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    [SerializeField] private GameObject[] skins;
    [SerializeField] private PlayerAnimationController animationController;
    private void Awake()
    {
        int skinNum = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.SkinNumber);
        GameObject skinGO = Instantiate(skins[skinNum], transform);
        animationController.SetAnimator(skinGO.GetComponent<Animator>());
    }
}
