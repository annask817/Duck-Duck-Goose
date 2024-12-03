using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DucklingManager : MonoBehaviour
{
    [SerializeField] public GameObject playerDuckPrefab;
    [SerializeField] public GameObject gameOver;
    public DucklingSpawner ducklingSpawner;
    public int ducklingCount = 0;
    public TextMeshProUGUI duckCountText;
    public bool gameEnd = false;
    

    void Start()
    {   
        // dubugging
        if (gameObject.name != "DucklingManager")
        {
            Debug.LogWarning("DucklingManager should be on the DucklingManager object!");
        }
        if (duckCountText == null)
        {
            Debug.LogError("Duck Count Text is not assigned! Please assign it in the Inspector.");
        }
        else
        {
            duckCountText.fontSize = 10;  
        }

        ducklingSpawner = FindObjectOfType<DucklingSpawner>();
        if (ducklingSpawner == null)
        {
            Debug.LogError("Could not find DucklingSpawner!");
        }

        ducklingSpawner =  FindObjectOfType<DucklingSpawner>();

        if (gameOver != null) 
        {
            gameOver.SetActive(false);
        }
    }

    void Update()
    {
        if (duckCountText != null)
        {
            duckCountText.text = "Collected Duckling(s): " + ducklingCount.ToString();
        }
        if (!gameEnd) 
        {
            int currentDucklings = GameObject.FindGameObjectsWithTag("Duckling").Length;

        }
    }

    public void CollectDuckling()
    {
        ducklingCount++;
        Debug.Log($"Duckling collected! Total count: {ducklingCount}");
    }

    public bool endGameCondition(int currentDucklings) 
    {
        return currentDucklings >= ducklingSpawner.REQUIRED_DUCKLINGS;
    }

    public void endGame() 
    {
        gameEnd = true;
        Debug.Log("Game Over! All ducklings collected!");

        if (gameOver != null) 
        {
            gameOver.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Game Over Screen Not Assigned!");
        }
        
    }
}