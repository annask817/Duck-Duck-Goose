using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandlingBoundaryCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Destroyable")) {
            Debug.Log("Duck collided with vehicle!");
            SceneManager.LoadScene("GameOver");
        }
    }    
}
