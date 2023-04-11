using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedWaypointController : WaypointController
{
    [SerializeField] private float waitTime;

    public float WaitTime => waitTime;
}
