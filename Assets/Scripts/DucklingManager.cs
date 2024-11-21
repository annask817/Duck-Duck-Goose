using UnityEngine;
using UnityEngine.UI;

public class DucklingManager : MonoBehaviour
{
    public int ducklingCount = 0;
    public Text duckCountText;
    [SerializeField] private GameObject playerDuckPrefab;

    void Start()
    {
        if (gameObject.name != "DucklingManager")
        {
            Debug.LogWarning("DucklingManager should be on the DucklingManager object!");
        }
    }

    void Update()
    {
        duckCountText.text = "Duckling Count: " + ducklingCount.ToString();
        if (ducklingCount == 5)
        {
            // end game
        }
    }

    public void CollectDuckling()
    {
        ducklingCount++;
        Debug.Log($"Duckling collected! Total count: {ducklingCount}");
    }
}