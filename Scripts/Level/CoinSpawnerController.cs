using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawnerController : MonoBehaviour
{
    [SerializeField] private CoinController coinPrefab;
    [SerializeField] private int minSpawn;
    [SerializeField] private int maxSpawn;
    [SerializeField] private Transform origin;
    [SerializeField] private float angleOffset;
    [SerializeField] private float force;

    public void SpawnMoney(bool isFloor)
    {
        int num = Random.Range(minSpawn, maxSpawn);
        Vector3 circle;
        for (int i = 0; i < num; i++)
        {
            Rigidbody rbGO = Instantiate(coinPrefab, origin.position, Quaternion.identity)
                .GetComponent<Rigidbody>();
            if (isFloor)
            {
                circle = Random.insideUnitCircle;
                circle.z = circle.y;
                circle.y = 0;
                circle += Vector3.up;
                circle.Normalize();
                rbGO.AddForce(circle * force, ForceMode.Impulse);
            }
            else
            {
                float angle = Random.Range(-angleOffset, angleOffset);
                rbGO.AddForce(Quaternion.AngleAxis(angle, Vector3.up) * origin.forward * force, ForceMode.Impulse);
            }
        }
    }
}