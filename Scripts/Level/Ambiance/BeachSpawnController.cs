using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject beachPrefab;
    [SerializeField] private GameObject plane;
    [SerializeField] private int levelsPerAmbiance;

    private void Awake()
    { 
        int level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level);
        if (level / levelsPerAmbiance % 2 == 1)
        {
            plane.SetActive(false);
            Instantiate(beachPrefab, transform);
        }
    }
}
