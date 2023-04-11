using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintsPool
{
    private Queue<FootprintController> _pool;
    private int _maxPool;

    private FootprintController _prefab;
    
    private FootprintController _spawnObject;
    
    public FootprintsPool(int maxPool, FootprintController objectPrefab)
    {
        _pool = new Queue<FootprintController>();
        _maxPool = maxPool;
        _prefab = objectPrefab;
    }

    public void AddToPool(FootprintController obj)
    {
        if (_pool.Count < _maxPool)
        {
            _pool.Enqueue(obj);
        }
    }

    public FootprintController GetObject(Vector3 position, Quaternion rotation)
    {
        if (_pool.Count > 0)
        {
            _spawnObject = _pool.Dequeue();
            var transform = _spawnObject.transform;
            transform.position = position;
            transform.rotation = rotation;
        }
        else
        {
            _spawnObject = Object.Instantiate(_prefab, position, rotation);
        }
        return _spawnObject;
    }
}
