using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAmbianceController : MonoBehaviour
{
    private Renderer _renderer;
    [SerializeField] private Material[] materials;
    [SerializeField] private int levelsPerAmbiance;

    private void Awake()
    {
        int level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level);
        if (level / levelsPerAmbiance % 2 == 1)
        {
            _renderer = GetComponent<MeshRenderer>();
            _renderer.materials = materials;
        }
    }
}
