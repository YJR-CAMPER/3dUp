using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Item Spawn Settings")]
    [SerializeField] private ItemData[] itemPool;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private bool randomize = true;

    private void Start()
    {
        SpawnAll();

        if (spawnInterval > 0f)
        {
            StartCoroutine(SpawnLoop());
        }
    }

    private void SpawnAll()
    {
        foreach (var point in spawnPoints)
        {
            SpawnItemAt(point.position);
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            foreach (var point in spawnPoints)
            {
                if (point.childCount == 0)
                    SpawnItemAt(point.position);
            }
        }
    }

    private void SpawnItemAt(Vector3 position)
    {
        if (itemPool.Length == 0) return;

        ItemData item = randomize
            ? itemPool[Random.Range(0, itemPool.Length)]
            : itemPool[0];

    if (item != null && item.prefab != null)
        {
            GameObject spawned = Instantiate(item.prefab, position, Quaternion.identity);
            spawned.name = $"[Item] {item.itemName}";
        }
    }
}
