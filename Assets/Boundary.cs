using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
        /*
        else
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.velocity = Vector3.zero;
            }
            // Alternatively, move the object back within the boundary
            Vector3 boundaryPosition = transform.position;
            Vector3 newPosition = other.transform.position;

            // Adjust the position if the object goes out of bounds (adjust axis as needed)
            if (newPosition.x > boundaryPosition.x)
            {
                newPosition.x = boundaryPosition.x;
            }
            if (newPosition.z > boundaryPosition.z)
            {
                newPosition.z = boundaryPosition.z;
            }

            other.transform.position = newPosition;
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
