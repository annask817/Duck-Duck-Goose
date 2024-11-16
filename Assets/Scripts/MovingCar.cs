using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCar : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 originalPosition;
    public float respawnDelay = 2f;
    public GameObject[] vehicleOptions;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        // Debug.Log(gameObject);
        // Debug.Log(originalPosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider collision) {
        // Check if the object collided with is the boundary by using a tag or name
        if (collision.CompareTag("InvisibleBoundary")) {
            // Perform any actions you want upon collision with the boundary
            Debug.Log("Vehicle collided with boundary!");
            Destroy(gameObject);
            int chosenIndex = Random.Range(0, vehicleOptions.Length);
            GameObject chosenVehicle = vehicleOptions[chosenIndex];
            MovingCar vehicleScript = chosenVehicle.GetComponent<MovingCar>();
            if (vehicleScript != null) {
                vehicleScript.enabled = true;
                vehicleScript.SetVehicleOptions(vehicleOptions);
                vehicleScript.SetSpeed(speed);
            }
            Instantiate(chosenVehicle, originalPosition, Quaternion.identity);
        }
    }

    public void SetVehicleOptions(GameObject[] newVehicleOptions) {
        vehicleOptions = newVehicleOptions;
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }
}
