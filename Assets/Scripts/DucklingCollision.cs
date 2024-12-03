using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucklingCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public DucklingManager dm;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Duckling"))
        {
            Debug.Log("Collecting duckling");

            Destroy(collision.gameObject);
            dm.ducklingCount++;

            Debug.Log($"Duckling count: {dm.ducklingCount}");
        }
    }
}