using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintsSpawner : MonoBehaviour
{
    [SerializeField] private FootprintController footprintObject;
    [SerializeField] private float spawnHeightOffset;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private int maxSpawnNumber = 20;
    
    [SerializeField] private float spawnPositionDelta;
    private int _numberSpawned;
    [SerializeField] private float footprintsLifeTime = 3f;
    private float _spawnDeltaSqrd;
    
    private Vector3 _oldPosition;
    
    private Vector3 _newPos;

    private FootprintsPool _pool;
    [SerializeField] private int poolNumber = 30;
    private bool _isActive;
    private void Awake()
    {
        _pool = new FootprintsPool(poolNumber, footprintObject);
        _spawnDeltaSqrd = spawnPositionDelta * spawnPositionDelta;
        EventManager.AddListener<PlayerPaintTrapEvent>(OnPaintTrap);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerPaintTrapEvent>(OnPaintTrap);

    }

    private void OnPaintTrap(PlayerPaintTrapEvent obj)
    {
        ActivatePrints();
    }

    private void Update()
    {
        if (_isActive && (playerTransform.position - _oldPosition).sqrMagnitude > _spawnDeltaSqrd)
        {
            SpawnFootprint();
        }
    }

    private void ActivatePrints()
    {
        _oldPosition = playerTransform.position;
        _numberSpawned = 0;
        _isActive = true;
    }
    private void SpawnFootprint()
    {
        FootprintController go = _pool.GetObject(playerTransform.position,
            Quaternion.LookRotation(playerTransform.forward));
        go.Initialize(footprintsLifeTime, _pool, playerTransform);
        _newPos = go.transform.position;
        _oldPosition = _newPos;
        _newPos.y += spawnHeightOffset;
        go.transform.position = _newPos;

        _numberSpawned++;
        if (_numberSpawned >= maxSpawnNumber)
        {
            _isActive = false;
        }
    }

}
