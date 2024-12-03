using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps; 

public class DucklingSpawner : MonoBehaviour
{
    [SerializeField] public GameObject ducklingPrefab;
    [SerializeField] public float checkRadius;
    [SerializeField] public float minDucklingDistance;
    public List<GameObject> spawnedDucklings = new List<GameObject>();
    // spawn amount
    public int REQUIRED_DUCKLINGS;

    // map size
    public float MAP_WIDTH = 45f;
    public float MAP_HEIGHT = 45f;    

    public bool isSpawning = false;

    public void Start()
    {
        
        // generate random amount of ducklings
        REQUIRED_DUCKLINGS = Random.Range(5, 21);
        Debug.Log($"Spawning {REQUIRED_DUCKLINGS} ducklings");

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
    

    // spawn ducklings
    public void SpawnDuckling(int i)
    {
        if (isSpawning)
        {
            Debug.LogWarning("SpawnDuckling() is already in progress.");
            return;
        }

        isSpawning = true;
        Debug.Log("SpawnDuckling() called.");

        GameObject parentDuckling = GameObject.Find("DucklingContainer");

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
            duckling.name = $"Duckling_{i}";
            spawnedDucklings.Add(duckling);
            Debug.Log($"Successfully spawned duckling {i + 1} at {spawnPosition}");
        }
        else
        {
            Debug.LogWarning($"Failed to spawn duckling {i + 1}. No valid position found.");
        }

        isSpawning = false;
    }

    // find spawn point
    public Vector3 FindValidSpawnPosition()
    {
        // attempts to validate spawn point
        int maxAttempts = 20;

        for (int i = 0; i < maxAttempts; i++)
        {
            float randomX = Random.Range(-MAP_WIDTH, 0f); 
            float randomZ = Random.Range(0f, MAP_HEIGHT);  
            
            Vector3 randomPosition = new Vector3(randomX, 1f, randomZ); 
            
            if (IsPositionValid(randomPosition))
            {
                Debug.Log($"Found valid position at attempt {i + 1}: {randomPosition}");
                return randomPosition;
            }
        }

        Debug.LogWarning("Could not find valid position after " + maxAttempts + " attempts");
        return Vector3.zero;
    }

    // validate spawn point
    public bool IsPositionValid(Vector3 position)
    {
        // check for obstacles 
        Collider[] colliders = Physics.OverlapBox(position, Vector3.one * checkRadius, Quaternion.identity, LayerMask.GetMask("Obstacle"));
        if (colliders.Length > 0)
        {
            Debug.Log($"Position {position} is blocked by an obstacle.");
            return false;
        }

        // check if the position is too close to other ducklings
        foreach (GameObject duckling in spawnedDucklings)
        {
            if (duckling != null && Vector3.Distance(duckling.transform.position, position) < minDucklingDistance)
            {
                Debug.Log($"Position {position} is too close to another duckling.");
                return false;
            }
        }

        return true;
    }

}