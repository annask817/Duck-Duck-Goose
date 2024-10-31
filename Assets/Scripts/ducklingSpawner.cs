using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucklingSpawner : MonoBehaviour {

    /* VARIABLES */

    // prefab for duckling to respawn
    public GameObject ducklingPrefab;

    // number of ducklings to spawn
    public int numOfDucklings = 0;

    // spawn area
    public Vector3 spawnAreaSize = new Vector3(10, 0 , 10);

    // y heaight to place ducklings on
    public float spawnHeight = 1.0f;

    // prevent spawning on obstacles
    public float spawnRadiusCheck = 1.5f;

    // identify obstacles to ensure duckling is not spawning too close to obstacle
    public LayerMask obstacleLayer;

    // spawning interval
    public float spawnInterval = 3.0f;
    private float timeSinceLastSpawn = 0.0f;

    /* FUNCTIONS */
    void Start() {
        SpawnDucklings();
    }

    void Update() {
        // increment time
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval) {
            SpawnDucklings();
            timeSinceLastSpawn = 0f; // reset timer
        }
    }

    void SpawnDucklings() {
        for (int i = 0; i < numOfDucklings; i++) {
            Vector3 spawnPosition;

            if (TryGetValidSpawnPosition(out spawnPosition)) {
                // spawn duckling at chosen position
                Instantiate(ducklingPrefab, spawnPosition, Quaternion.identity);
                // 'Quaternion.identity' -> we can change so ducklings can face random directions when spawned
            }   
            else {
                Debug.LogWarning("Error: cannot find span position for " + i);
            }
        }
    }

    // find valid position to spawn
    bool TryGetValidSpawnPosition(out Vector3 result) {

        // attempts to find a valid position
        // using 10 for now
        int attempts = 10; 

        for (int i = 0; i < attempts; i++) {
            Vector3 randomPosition = new Vector3 (
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                spawnHeight, // y is given
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            // check if spawn location is clear of obstacles
            if (!Physics.CheckSphere(randomPosition, spawnRadiusCheck, obstacleLayer)) {
                result = randomPosition;
                return true; // position found
            }
        }

        result = Vector3.zero;
        return false;
    }

    // helps visualize spawn area
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }

}
