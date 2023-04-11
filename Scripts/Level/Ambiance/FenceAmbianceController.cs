using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceAmbianceController : MonoBehaviour
{
    private MeshRenderer _renderer;
    [SerializeField] private GameObject secondFence;
    [SerializeField] private int levelsPerAmbiance;
    private void Awake()
    {
        int level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level);
        if (level / levelsPerAmbiance % 2 == 1)
        {
            _renderer = GetComponent<MeshRenderer>();
            _renderer.enabled = false;
            secondFence.SetActive(true);
        }
    }
}
