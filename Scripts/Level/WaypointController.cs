using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    [SerializeField] private float yOffset;
    private Vector3 _position;
    public Vector3 Point => _position + yOffset * Vector3.up;
    private void Awake()
    {
        _position = transform.position;
    }

    public void ChangePos(Vector3 newPosition)
    {
        newPosition.y = 0;
        transform.position = newPosition;
        _position = newPosition;
    }
}
