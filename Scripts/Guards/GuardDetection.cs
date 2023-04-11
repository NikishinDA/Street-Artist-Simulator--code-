using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


public class GuardDetection : MonoBehaviour
{
    [SerializeField] private float spottingAngle;
    [SerializeField] private Transform spottingOrigin;
    [SerializeField] private float spottingDistance;
    [SerializeField] private LayerMask detectLayerMask;
    [SerializeField] private LayerMask visionLayerMask;

    private Collider _trigger;
    private Transform _lockedTarget;

    public event Action<Transform> TargetSighted;


    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }


    public void ToggleTrigger(bool toggle)
    {
        _trigger.enabled = toggle;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Vector3.Angle(transform.forward, other.transform.position - transform.position) > spottingAngle /2) return;
        _lockedTarget = CheckVisualContact(other.transform);
        if (_lockedTarget)
            TargetSighted?.Invoke(_lockedTarget);
    }


    public Transform CheckVisualContact(Transform otherTransform)
    {
        if (!otherTransform) return null;
        RaycastHit hit;
        if (Physics.Raycast(spottingOrigin.position,
            otherTransform.position - spottingOrigin.position,
            out hit,
            spottingDistance,
            visionLayerMask))
        {
            if (detectLayerMask == (detectLayerMask | (1 << hit.collider.gameObject.layer)))
            {
                Debug.DrawRay(spottingOrigin.position,
                    hit.distance * (otherTransform.position - spottingOrigin.position).normalized, Color.green, 10f);
                return otherTransform;
            }
        }

        Debug.DrawRay(spottingOrigin.position,
           spottingDistance * (otherTransform.position - spottingOrigin.position).normalized, Color.red, 10f);

        return null;
    }
}
