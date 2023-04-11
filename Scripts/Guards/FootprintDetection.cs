using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintDetection : MonoBehaviour
{    
    [SerializeField] private float spottingAngle;
    [SerializeField] private Transform spottingOrigin;
    [SerializeField] private float spottingDistance;
    [SerializeField] private LayerMask detectLayerMask;
    [SerializeField] private LayerMask visionLayerMask;
    [SerializeField] private int frameSkip = 1;
    private Collider _trigger;

    private Transform _playerTransform;
    
    public event Action<Transform> FootprintSpotted;

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
        if (Time.frameCount % frameSkip == 0)        
            if (Vector3.Angle(transform.forward, other.transform.position - transform.position) > spottingAngle /2) return;
            else
                if (CheckVisualContact(other.transform)) 
                    FootprintSpotted?.Invoke(_playerTransform);
    }
    public bool CheckVisualContact(Transform otherTransform)
    {
        if (!otherTransform) return false;
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
                    hit.distance * (otherTransform.position - spottingOrigin.position).normalized, Color.blue, 10f);
                if (!_playerTransform)
                    _playerTransform = hit.collider.GetComponent<FootprintHurtbox>().GetPlayerTransform();
                return true;
            }
        }

        Debug.DrawRay(spottingOrigin.position,
            spottingDistance * (otherTransform.position - spottingOrigin.position).normalized, Color.yellow, 10f);

        return false;
    }
}
