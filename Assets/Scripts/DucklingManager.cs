using UnityEngine;

public class DucklingManager : MonoBehaviour
{
    public int ducklingCount = 0;
    [SerializeField] private GameObject playerDuckPrefab;

    void Start()
    {
        if (gameObject.name != "DucklingManager")
        {
            Debug.LogWarning("DucklingManager should be on the GameManager object!");
        }
    }

    public void CollectDuckling()
    {
        ducklingCount++;
        Debug.Log($"Duckling collected! Total count: {ducklingCount}");
    }
}