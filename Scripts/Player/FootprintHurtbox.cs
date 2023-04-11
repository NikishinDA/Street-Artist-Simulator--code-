using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintHurtbox : MonoBehaviour
{
    [SerializeField] private FootprintController controller;

    public Transform GetPlayerTransform()
    {
        return controller.PlayerRef;
    }
}
