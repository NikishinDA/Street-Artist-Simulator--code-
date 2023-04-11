using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinAmbianceController : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] renderers;
    [SerializeField] private Material secondMaterial;
    [SerializeField] private int levelsPerAmbiance;

    private void Awake()
    {
        int level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level);
        if (level / levelsPerAmbiance % 2 == 1)
        {
            foreach (var meshRenderer in renderers)
            {
                meshRenderer.material = secondMaterial;
            }
        }
    }
}
