using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected MeshRenderer meshRenderer;
    [HideInInspector] public bool occupied;
    [HideInInspector] public Vector2 gridPosition;
    public Transform pivot;
    [SerializeField] GameObject[] obstaclePrefabs;

    public abstract void Init(Vector2 pos);

    public void SpawnObstacle()
    {
        GameObject randomObstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Instantiate(randomObstacle, pivot.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
        occupied = true;
    }
}
