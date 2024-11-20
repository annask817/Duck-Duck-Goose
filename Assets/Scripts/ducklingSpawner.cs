using UnityEngine;
using System.Collections.Generic;

public class DucklingSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ducklingPrefab;
    private const int REQUIRED_DUCKLINGS = 5;

    private const float MAP_WIDTH = 100f;
    private const float MAP_HEIGHT = 100f;

    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float checkRadius = 1f;
    [SerializeField] private float minDucklingDistance = 2f;

    private List<GameObject> spawnedDucklings = new List<GameObject>();
    //private bool isSpawningComplete = false;
    private bool isSpawning = false;

    private void Start()
    {
        if (ducklingPrefab == null)
        {
            Debug.LogError("Duckling Prefab is not assigned!");
            return;
        }

        for (int i = 0; i < REQUIRED_DUCKLINGS; i++)
        {
            SpawnDuckling(i);
        }
    }

    private void SpawnDuckling(int i)
    {
        if (isSpawning)
        {
            Debug.LogWarning("SpawnDuckling() is already in progress.");
            return;
        }

        isSpawning = true;
        Debug.Log("SpawnDuckling() called.");

        GameObject parentDuckling = GameObject.Find("Duckling");
        if (parentDuckling == null)
        {
            Debug.LogError("Parent Duckling not found!");
            isSpawning = false;
            return;
        }

        Vector3 spawnPosition = FindValidSpawnPosition();
        if (spawnPosition != Vector3.zero)
        {
            GameObject duckling = Instantiate(ducklingPrefab, spawnPosition, Quaternion.identity, parentDuckling.transform);
            duckling.name = "Duckling" + i.ToString();
            duckling.transform.localScale = new Vector3(5f, 5f, 5f);
            //duckling.layer = LayerMask.NameToLayer("Duckling");
            spawnedDucklings.Add(duckling);
            Debug.Log($"Successfully spawned duckling at {spawnPosition}");
        }
        else
        {
            Debug.LogWarning("Failed to find valid spawn position for duckling.");
        }

        isSpawning = false;
    }

    private Vector3 FindValidSpawnPosition()
    {
        int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            float randomX = Random.Range(-MAP_WIDTH / 2, MAP_WIDTH / 2);
            float randomZ = Random.Range(-MAP_HEIGHT / 2, MAP_HEIGHT / 2);
            Vector3 randomPosition = new Vector3(randomX, 0, randomZ);

            if (IsPositionValid(randomPosition))
            {
                Debug.Log($"Found valid position at attempt {i + 1}: {randomPosition}");
                return randomPosition;
            }
        }

        Debug.LogWarning("Could not find valid position after " + maxAttempts + " attempts");
        return Vector3.zero;
    }

    private bool IsPositionValid(Vector3 position)
    {
        if (Physics.CheckSphere(position, checkRadius, obstacleLayer))
        {
            Debug.Log($"Position {position} is blocked by an obstacle.");
            return false;
        }

        foreach (GameObject duckling in spawnedDucklings)
        {
            if (duckling != null &&
                Vector3.Distance(duckling.transform.position, position) < minDucklingDistance)
            {
                Debug.Log($"Position {position} is too close to another duckling.");
                return false;
            }
        }
        return true;
    }
}