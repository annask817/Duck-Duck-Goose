using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public GameObject vehiclePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroyable"))
        {
            Debug.Log("Car collided with boundary!");
            // Debug.Log(other.gameObject.GetComponent<MovingCar>());
            // other.gameObject.SetActive(false);
            // Destroy(other.gameObject);
            // Vector3 position = new Vector3(-17.82f, 2.342f, 0.59f);
            // GameObject newVehicle = Instantiate(vehiclePrefab, Vector3.zero, Quaternion.identity);
            // MovingCar carScript = newVehicle.GetComponent<MovingCar>();
            // Vector3 vehiclePosition = carScript.originalPosition;
            // newVehicle.transform.position = vehiclePosition;
        }
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
